using e_commerce_api.Models;
using e_commerce_api.Repository;
using e_commerce_api.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;

	public OrderService(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	public async Task<Order> PlaceOrderAsync(int userId, List<OrderItemDto> orderItemDtos)
	{
		var orderItems = orderItemDtos.Select(dto => new OrderItem
		{
			ProductId = dto.ProductId,
			Quantity = dto.Quantity,
			UnitPrice = dto.UnitPrice
		}).ToList();

		var order = new Order
		{
			UserId = userId,
			OrderDate = DateTime.UtcNow,
			TotalAmount = orderItems.Sum(i => i.Quantity * i.UnitPrice),
			Status = "Pending",
			OrderItems = orderItems
		};

		return await _orderRepository.CreateAsync(order);
	}

	public async Task<Order> GetOrderByIdAsync(int id)
	{
		return await _orderRepository.GetByIdAsync(id);
	}

	public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
	{
		return await _orderRepository.GetByUserIdAsync(userId);
	}

	public async Task UpdateOrderStatusAsync(int id, string status)
	{
		var order = await _orderRepository.GetByIdAsync(id);
		if (order == null) throw new ArgumentException("Order not found");

		order.Status = status;
		await _orderRepository.UpdateAsync(order);
	}
}
