using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHub.Models
{
	public class User
	{
		[Key]
		[Column("ID")]
		public int Id { get; set; }
		[Column("first_name")]
		[StringLength(50)]
		public string Name { get; set; }
		[Column("last_name")]
		[StringLength(50)]
		public string Surname { get; set; }
		[Column("alias")]
		[StringLength(30)]
		public string Alias { get; set; }
		[Column("email")]
		[StringLength(255)]
		public string Email { get; set; }
		[Column("password")]
		[StringLength(255)]
		public string Password { get; set; }
		[Column("is_admin")]
		public bool isAdmin { get; set; }
	}
}
