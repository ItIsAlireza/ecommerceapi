using e_commerce_api.Data;
using e_commerce_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SeedData
{
	private readonly ApplicationDbContext _context;
	private readonly PasswordHasher<User> _passwordHasher;

	public SeedData(ApplicationDbContext context)
	{
		_context = context;
		_passwordHasher = new PasswordHasher<User>();
	}

	public async Task InitializeAsync()
	{
		if (await _context.Roles.AnyAsync())
		{
			return; // Data has already been seeded
		}

		// Seed Roles
		_context.Roles.AddRange(
			new Role { Name = "Admin" },
			new Role { Name = "User" }
		);

		await _context.SaveChangesAsync();

		// Seed Users
		var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
		var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");

		if (!await _context.Users.AnyAsync())
		{
			_context.Users.AddRange(
				new User
				{
					Username = "admin",
					PasswordHash = _passwordHasher.HashPassword(null, "admin_password"),  // Use actual password
					UserRoles = new List<UserRole> { new UserRole { Role = adminRole } }
				},
				new User
				{
					Username = "user",
					PasswordHash = _passwordHasher.HashPassword(null, "user_password"),  // Use actual password
					UserRoles = new List<UserRole> { new UserRole { Role = userRole } }
				}
			);

			await _context.SaveChangesAsync();
		}
	}
}
