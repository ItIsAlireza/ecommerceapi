using e_commerce_api.Data;
using e_commerce_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_commerce_api.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetAllAsync(int skip, int take, string sortBy, bool ascending, decimal? minPrice, decimal? maxPrice)
		{
			IQueryable<Product> query = _context.Products;

			if (minPrice.HasValue)
				query = query.Where(p => p.Price >= minPrice.Value);
			if (maxPrice.HasValue)
				query = query.Where(p => p.Price <= maxPrice.Value);

			Expression<Func<Product, object>> keySelector = sortBy.ToLower() switch
			{
				"price" => p => p.Price,
				"stockquantity" => p => p.StockQuantity,
				_ => p => p.Name
			};

			query = ascending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);

			return await query.Skip(skip).Take(take).ToListAsync();
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			return await _context.Products.FindAsync(id);
		}

		public async Task<Product> CreateAsync(Product product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return product;
		}

		public async Task UpdateAsync(Product product)
		{
			_context.Entry(product).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Product>> SearchAsync(string query)
		{
			return await _context.Products
				.Where(p => p.Name.Contains(query) || p.Description.Contains(query))
				.Take(10)
				.ToListAsync();
		}

		public async Task<int> GetTotalCountAsync(decimal? minPrice, decimal? maxPrice)
		{
			IQueryable<Product> query = _context.Products;

			if (minPrice.HasValue)
				query = query.Where(p => p.Price >= minPrice.Value);
			if (maxPrice.HasValue)
				query = query.Where(p => p.Price <= maxPrice.Value);

			return await query.CountAsync();
		}
	}
}