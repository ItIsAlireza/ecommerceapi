using System.ComponentModel.DataAnnotations;

namespace e_commerce_api.Models
{
	public class Role
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public List<UserRole> UserRoles { get; set; }
	}
}
