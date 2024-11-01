using Microsoft.EntityFrameworkCore;
using e_commerce_api.Models;

namespace e_commerce_api.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<User> Users => Set<User>();

		public DbSet<Product> Products => Set<Product>();

		public DbSet<Role> Roles => Set<Role>();

		public DbSet<UserRole> UserRoles => Set<UserRole>();

		public DbSet<Order> Orders => Set<Order>();

		public DbSet<OrderItem> OrderItems => Set<OrderItem>();


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed data with actual product names and details
			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "Samsung Galaxy S21", Description = "Latest Samsung flagship smartphone with 8GB RAM and 128GB storage.", Price = 799.99m, StockQuantity = 50 },
				new Product { Id = 2, Name = "Apple MacBook Pro", Description = "Apple MacBook Pro with M1 chip, 16GB RAM, and 512GB SSD.", Price = 1299.99m, StockQuantity = 30 },
				new Product { Id = 3, Name = "Sony WH-1000XM4", Description = "Noise-cancelling wireless headphones with 30-hour battery life.", Price = 349.99m, StockQuantity = 100 },
				new Product { Id = 4, Name = "Dell XPS 13", Description = "Dell XPS 13 laptop with 11th Gen Intel Core i7, 16GB RAM, and 1TB SSD.", Price = 1499.99m, StockQuantity = 20 },
				new Product { Id = 5, Name = "GoPro HERO9", Description = "GoPro HERO9 action camera with 5K video and 20MP photos.", Price = 399.99m, StockQuantity = 75 },
				new Product { Id = 6, Name = "Nintendo Switch", Description = "Nintendo Switch console with Neon Blue and Neon Red Joy‑Con.", Price = 299.99m, StockQuantity = 150 },
				new Product { Id = 7, Name = "Apple AirPods Pro", Description = "Apple AirPods Pro with Active Noise Cancellation and Wireless Charging Case.", Price = 249.99m, StockQuantity = 200 },
				new Product { Id = 8, Name = "Canon EOS R6", Description = "Canon EOS R6 full-frame mirrorless camera with 4K video and 20.1MP sensor.", Price = 2499.99m, StockQuantity = 10 },
				new Product { Id = 9, Name = "Bose SoundLink Revolve", Description = "Bose SoundLink Revolve portable Bluetooth speaker with 360-degree sound.", Price = 199.99m, StockQuantity = 120 },
				new Product { Id = 10, Name = "Fitbit Charge 4", Description = "Fitbit Charge 4 fitness and activity tracker with built-in GPS.", Price = 149.99m, StockQuantity = 250 }
			);

			// Define primary key for UserRole
			modelBuilder.Entity<UserRole>()
				.HasKey(ur => new { ur.UserId, ur.RoleId });

			// Ensure UserRole entries are unique
			modelBuilder.Entity<UserRole>()
				.HasIndex(ur => new { ur.UserId, ur.RoleId })
				.IsUnique();

			// Configure relationships
			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.User)
				.WithMany(u => u.UserRoles)
				.HasForeignKey(ur => ur.UserId);

			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.RoleId);
		}


	}
}
