using e_commerce_api.Models;
using e_commerce_api.Repositories;

namespace e_commerce_api.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize, string sortBy, bool ascending, decimal? minPrice, decimal? maxPrice)
		{
			int skip = (page - 1) * pageSize;
			return await _productRepository.GetAllAsync(skip, pageSize, sortBy, ascending, minPrice, maxPrice);
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _productRepository.GetByIdAsync(id);
		}

		public async Task<Product> CreateProductAsync(Product product)
		{
			return await _productRepository.CreateAsync(product);
		}

		public async Task UpdateProductAsync(Product product)
		{
			await _productRepository.UpdateAsync(product);
		}

		public async Task DeleteProductAsync(int id)
		{
			await _productRepository.DeleteAsync(id);
		}

		public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
		{
			return await _productRepository.SearchAsync(query);
		}
	}
}