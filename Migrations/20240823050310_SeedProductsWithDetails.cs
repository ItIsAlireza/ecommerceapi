using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace e_commerce_api.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductsWithDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Latest Samsung flagship smartphone with 8GB RAM and 128GB storage.", "Samsung Galaxy S21", 799.99m, 50 },
                    { 2, "Apple MacBook Pro with M1 chip, 16GB RAM, and 512GB SSD.", "Apple MacBook Pro", 1299.99m, 30 },
                    { 3, "Noise-cancelling wireless headphones with 30-hour battery life.", "Sony WH-1000XM4", 349.99m, 100 },
                    { 4, "Dell XPS 13 laptop with 11th Gen Intel Core i7, 16GB RAM, and 1TB SSD.", "Dell XPS 13", 1499.99m, 20 },
                    { 5, "GoPro HERO9 action camera with 5K video and 20MP photos.", "GoPro HERO9", 399.99m, 75 },
                    { 6, "Nintendo Switch console with Neon Blue and Neon Red Joy‑Con.", "Nintendo Switch", 299.99m, 150 },
                    { 7, "Apple AirPods Pro with Active Noise Cancellation and Wireless Charging Case.", "Apple AirPods Pro", 249.99m, 200 },
                    { 8, "Canon EOS R6 full-frame mirrorless camera with 4K video and 20.1MP sensor.", "Canon EOS R6", 2499.99m, 10 },
                    { 9, "Bose SoundLink Revolve portable Bluetooth speaker with 360-degree sound.", "Bose SoundLink Revolve", 199.99m, 120 },
                    { 10, "Fitbit Charge 4 fitness and activity tracker with built-in GPS.", "Fitbit Charge 4", 149.99m, 250 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
