using Microsoft.EntityFrameworkCore;
using e_commerce_api.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using e_commerce_api;
using e_commerce_api.Repositories;
using e_commerce_api.Services;
using e_commerce_api.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

int[] arr = new  int[]{
1,2,0,3,1,0,4};

arr.OrderByDescending(x => x);

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{

	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidIssuer = jwtSettings["Issuer"],
		ValidAudience = jwtSettings["Audience"],
		ClockSkew = TimeSpan.Zero // Optional: removes the default 5 minutes of clock skew
	};
});

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<SeedData>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>(); // Register RoleRepository
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderRepository, OrderRespository>();
builder.Services.AddScoped<IOrderService, OrderService>();


var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
	var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
	await seedData.InitializeAsync();  // Call InitializeAsync
}

app.UseHttpsRedirection();

// Use authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
