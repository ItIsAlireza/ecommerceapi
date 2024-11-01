using e_commerce_api.Models;

namespace e_commerce_api.Repository
{
	public interface IOrderRepository
	{

		Task<Order> CreateAsync(Order order);

		Task<Order> GetByIdAsync(int id);

		Task<IEnumerable<Order>> GetByUserIdAsync(int userId);

		Task UpdateAsync(Order order);
	}
}
