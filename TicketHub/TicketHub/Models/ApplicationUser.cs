using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHub.Models
{
	public class ApplicationUser : IdentityUser
	{
		
		[Required]
		[Column("first_name")]
		[StringLength(50)]
		public string? FirstName { get; set; }
		[Required]
		[Column("last_name")]
		[StringLength(50)]
		public string? LastName { get; set; }
		[Required]
		[Column("alias")]
		[StringLength(30)]
		public string? Alias { get; set; }

		[Column("is_admin")]
		public bool IsAdmin { get; set; }

		public ICollection<PurchaseHistory>? PurchaseHistories { get; set; }
		public ICollection<SaleHistory>? SaleHistories { get; set; }
	}
}
