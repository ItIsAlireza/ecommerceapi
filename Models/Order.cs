namespace e_commerce_api.Models
{
    public class Order
    {

        public int Id { get; set; }

        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public User User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }

	public class OrderItemDto
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
