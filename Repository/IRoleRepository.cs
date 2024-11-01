using e_commerce_api.Models;

namespace e_commerce_api.Repository
{
	public interface IRoleRepository
	{
		Task<Role> GetByNameAsync(string roleName);
	}

}
