using e_commerce_api.Data;
using e_commerce_api.Models;
using e_commerce_api.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class RoleRepository : IRoleRepository
{
	private readonly ApplicationDbContext _context;

	public RoleRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Role> GetByNameAsync(string roleName)
	{
		return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
	}
}
