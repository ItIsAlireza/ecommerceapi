using e_commerce_api.Data;
using e_commerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_api.Repository
{
	public class OrderRespository : IOrderRepository
	{

		private readonly ApplicationDbContext _context;

		public OrderRespository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Order> CreateAsync(Order order)
		{
			_context.Orders.Add(order);

			await _context.SaveChangesAsync();
			return order;
		}

		public async Task<Order> GetByIdAsync(int id)
		{
			return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
		}

		public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
		{
			return await _context.Orders.Include(o => o.OrderItems).Where(o => o.UserId == userId).ToListAsync();
		}

		public async Task UpdateAsync(Order order)
		{

			_context.Orders.Update(order);
			await _context.SaveChangesAsync();
		}
	}
}
