﻿using e_commerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_api.Data
{
	public class ECommerceDbContext : DbContext
	{
		public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

		public DbSet<User> Users { get; set; }

	}
}
