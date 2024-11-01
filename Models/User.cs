using System.ComponentModel.DataAnnotations;

namespace e_commerce_api.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Username { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		public List<UserRole> UserRoles { get; set; }
	}
}