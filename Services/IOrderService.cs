using e_commerce_api.Models;

namespace e_commerce_api.Services
{
	public interface IOrderService
	{
		Task<Order> PlaceOrderAsync(int userId, List<OrderItemDto> orderItemDtos);

		Task<Order> GetOrderByIdAsync(int id);
		Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
		Task UpdateOrderStatusAsync(int id, string status);
	}
}
