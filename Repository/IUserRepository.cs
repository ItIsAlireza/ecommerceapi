using e_commerce_api.Models;

namespace e_commerce_api.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetByIdAsync(int id);
		Task<User> GetByUsernameAsync(string username);
		Task<User> CreateAsync(User user);
		Task UpdateAsync(User user);  // Add this method
	}
}