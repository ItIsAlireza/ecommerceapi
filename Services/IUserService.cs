// IUserService.cs
using e_commerce_api.Models;

namespace e_commerce_api.Services
{
	public interface IUserService
	{
		Task<User> RegisterAsync(string username, string password, string role);

		Task<User> AuthenticateAsync(string username, string password);

		Task<User> GetUserByIdAsync(int id);

		Task<User> UpdateUserAsync(int id, UpdateUserModel updateUserModel);
		Task AssignRoleAsync(int userId, string roleName);

	}
}