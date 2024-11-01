using e_commerce_api.Models;

namespace e_commerce_api.Repositories
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetAllAsync(int skip, int take, string sortBy, bool ascending, decimal? minPrice, decimal? maxPrice);
		Task<Product> GetByIdAsync(int id);
		Task<Product> CreateAsync(Product product);
		Task UpdateAsync(Product product);
		Task DeleteAsync(int id);
		Task<IEnumerable<Product>> SearchAsync(string query);
		Task<int> GetTotalCountAsync(decimal? minPrice, decimal? maxPrice);
	}
}