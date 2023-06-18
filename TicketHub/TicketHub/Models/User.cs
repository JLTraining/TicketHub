
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHub.Models
{
	public class User 
	{
		[Key]
		[Column("ID")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[Column("first_name")]
		[StringLength(50)]
		public string FirstName { get; set; }
		[Required]
		[Column("last_name")]
		[StringLength(50)]
		public string LastName { get; set; }
		[Required]
		[Column("alias")]
		[StringLength(30)]
		public string Alias { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[Column("password")]
		[StringLength(255)]
		public string Password { get; set; }
		[Column("is_admin")]
		public bool IsAdmin { get; set; }

		public ICollection<PurchaseHistory> PurchaseHistories { get; set; }
		public ICollection<SaleHistory> SaleHistories { get; set; }
	}
}
