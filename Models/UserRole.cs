using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_api.Models
{
	[PrimaryKey(nameof(UserId), nameof(RoleId))]
	public class UserRole
	{
		public int UserId { get; set; }
		public User User { get; set; }

		public int RoleId { get; set; }
		public Role Role { get; set; }
	}
}