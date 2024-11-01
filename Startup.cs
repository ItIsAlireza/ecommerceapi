//using e_commerce_api.Data;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;

//namespace e_commerce_api
//{
//	public class Startup
//	{
//		public void ConfigureServices(IServiceCollection services)
//		{
//			services.AddDbContext<ApplicationDbContext>(options =>
//				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//			services.AddControllers();
//			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//				.AddJwtBearer(options =>
//				{
//					options.TokenValidationParameters = new TokenValidationParameters
//					{
//						ValidateIssuer = true,
//						ValidateAudience = true,
//						ValidateLifetime = true,
//						ValidateIssuerSigningKey = true,
//						ValidIssuer = Configuration["Jwt:Issuer"],
//						ValidAudience = Configuration["Jwt:Audience"],
//						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]))
//					};
//				});

//			services.AddSwaggerGen(c =>
//			{
//				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
//			});
//		}

//	}
//}
