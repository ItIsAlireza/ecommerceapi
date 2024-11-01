using e_commerce_api.Models;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		// GET: api/products
		[HttpGet]
		public async Task<ActionResult<ProductListResponse>> GetProducts(
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 10,
			[FromQuery] string sortBy = "name",
			[FromQuery] bool ascending = true,
			[FromQuery] decimal? minPrice = null,
			[FromQuery] decimal? maxPrice = null)
		{
			var products = await _productService.GetProductsAsync(page, pageSize, sortBy, ascending, minPrice, maxPrice);

			var totalItems = products.Count();  // Assuming you modify the service to return a List<Product>
			var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

			var response = new ProductListResponse
			{
				TotalItems = totalItems,
				TotalPages = totalPages,
				CurrentPage = page,
				PageSize = pageSize,
				Products = products.ToList()  // Assuming service returns IEnumerable<Product>
			};

			return Ok(response);
		}

		// GET: api/products/search
		[HttpGet("search")]
		public async Task<ActionResult<List<Product>>> SearchProducts([FromQuery] string query)
		{
			if (string.IsNullOrWhiteSpace(query))
				return BadRequest("Search query is required");

			var products = await _productService.SearchProductsAsync(query);

			return Ok(products);
		}

		// GET: api/products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		// POST: api/products
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<Product>> CreateProduct(Product product)
		{
			var createdProduct = await _productService.CreateProductAsync(product);

			return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
		}

		// PUT: api/products/5
		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult> UpdateProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			try
			{
				await _productService.UpdateProductAsync(product);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(await ProductExists(id)))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// DELETE: api/products/5
		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			await _productService.DeleteProductAsync(id);

			return NoContent();
		}

		private async Task<bool> ProductExists(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			return product != null;
		}
	}

	public class ProductListResponse
	{
		public int TotalItems { get; set; }
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public List<Product> Products { get; set; }
	}
}
