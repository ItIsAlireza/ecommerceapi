using e_commerce_api.Models;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerce_api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IConfiguration _configuration;
		private readonly ILogger<UsersController> _logger;

		public UsersController(IUserService userService, IConfiguration configuration, ILogger<UsersController> logger)
		{
			_userService = userService;
			_configuration = configuration;
			_logger = logger;
		}

		// POST: api/users/register
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var user = await _userService.RegisterAsync(model.Username, model.Password, model.Role);
				return CreatedAtAction(nameof(Register), new { id = user.Id }, new { user.Id, user.Username });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Registration failed for username {Username}.", model.Username);
				return BadRequest("Registration failed.");
			}
		}

		// POST: api/users/login
		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await _userService.AuthenticateAsync(loginModel.Username, loginModel.Password);

			if (user == null)
			{
				_logger.LogWarning("Failed login attempt for {Username}.", loginModel.Username);
				return Unauthorized("Invalid username or password.");
			}

			// Generate JWT
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim(ClaimTypes.Role, string.Join(",", user.UserRoles.Select(ur => ur.Role.Name)))
				}),
				Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"])),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			_logger.LogInformation("User {Username} logged in successfully.", user.Username);
			return Ok(new { Token = tokenString });
		}

		// GET: api/users/profile
		[HttpGet("profile")]
		public async Task<IActionResult> GetProfile()
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);

			if (string.IsNullOrEmpty(userId))
			{
				_logger.LogWarning("Invalid token provided.");
				return Unauthorized("Invalid Token");
			}

			var user = await _userService.GetUserByIdAsync(int.Parse(userId));

			if (user == null)
			{
				_logger.LogWarning("User not found with ID {UserId}.", userId);
				return NotFound("User not found");
			}

			_logger.LogInformation("Profile retrieved for user {Username}.", user.Username);
			return Ok(new
			{
				user.Id,
				user.Username,
				Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
			});
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var user = await _userService.UpdateUserAsync(id, model);
				return Ok(new { user.Id, user.Username });
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("{id}/assign-role")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AssignRole(int id, [FromBody] string roleName)
		{
			if (string.IsNullOrEmpty(roleName))
			{
				return BadRequest("Role name is required.");
			}

			try
			{
				await _userService.AssignRoleAsync(id, roleName);
				return Ok();
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}

	// RegisterModel and LoginModel classes
	public class RegisterModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Role { get; set; } // Optional
	}

	public class LoginModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
