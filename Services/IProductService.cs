using e_commerce_api.Models;

namespace e_commerce_api.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize, string sortBy, bool ascending, decimal? minPrice, decimal? maxPrice);
		Task<Product> GetProductByIdAsync(int id);
		Task<Product> CreateProductAsync(Product product);
		Task UpdateProductAsync(Product product);
		Task DeleteProductAsync(int id);
		Task<IEnumerable<Product>> SearchProductsAsync(string query);
	}
}
