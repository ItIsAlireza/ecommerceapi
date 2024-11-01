using e_commerce_api.Models;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace e_commerce_api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IUserService _userService;
		public OrdersController(IOrderService orderService, IUserService userService)
		{
			_orderService = orderService;
			_userService = userService;
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> PlaceOrder([FromBody] List<OrderItemDto> orderItemDtos)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);
			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized("Invalid Token");
			}

			var user = await _userService.GetUserByIdAsync(int.Parse(userId));
			if (user == null)
			{
				return NotFound("User not found");
			}

			int userIdInt = int.Parse(userId);
			var order = await _orderService.PlaceOrderAsync(userIdInt, orderItemDtos);
			return Ok(order);
		}
	}
}
