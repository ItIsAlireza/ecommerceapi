using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_api.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Product name is required")]
		[StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters")]
		public string Name { get; set; }


		[StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
		public string Description { get; set; }


		[Required(ErrorMessage = "Price is required")]
		[Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Stock quantity is required")]
		[Range(0, 1000, ErrorMessage = "Stock quantity must be between 0 and 1000")]
		public int StockQuantity { get; set; }
	}
}