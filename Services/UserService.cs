using e_commerce_api.Models;
using e_commerce_api.Repositories;
using e_commerce_api.Repository;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_api.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly PasswordHasher<User> _passwordHasher;
		private readonly IRoleRepository _roleRepository;

		public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_passwordHasher = new PasswordHasher<User>();
		}


		public async Task<User> RegisterAsync(string username, string password, string role)
		{
			var existingRole = await _roleRepository.GetByNameAsync(role); // Get the existing role

			if (existingRole == null)
			{
				throw new ArgumentException("Role not found");
			}

			var user = new User
			{
				Username = username,
				PasswordHash = _passwordHasher.HashPassword(null, password), // Use proper password hashing
				UserRoles = new List<UserRole> { new UserRole { Role = existingRole } }
			};

			return await _userRepository.CreateAsync(user);
		}

		public async Task<User> AuthenticateAsync(string username, string password)
		{
			var user = await _userRepository.GetByUsernameAsync(username);

			if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Success)
			{
				return null;
			}

			return user;
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			return await _userRepository.GetByIdAsync(id);
		}


		public async Task<User> UpdateUserAsync(int id, UpdateUserModel updateUserModel)
		{
			var user = await _userRepository.GetByIdAsync(id);
			if (user == null)
			{
				throw new ArgumentException("User not found");
			}

			user.Username = updateUserModel.Username;

			// Assuming you have a role repository for handling roles
			var role = await _roleRepository.GetByNameAsync(updateUserModel.Role);
			if (role == null)
			{
				throw new ArgumentException("Role not found");
			}


			user.UserRoles = new List<UserRole> { new UserRole { Role = role } };
			await _userRepository.UpdateAsync(user);

			return user;
		}

		public async Task AssignRoleAsync(int userId, string roleName)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			if (user == null)
			{
				throw new ArgumentException("User not found");
			}

			var role = await _roleRepository.GetByNameAsync(roleName);

			if (role == null)
			{
				throw new ArgumentException("Role not found");
			}

			user.UserRoles = new List<UserRole> { new UserRole { Role = role } };
			await _userRepository.UpdateAsync(user);


			await _userRepository.UpdateAsync(user);

		}






	}


}