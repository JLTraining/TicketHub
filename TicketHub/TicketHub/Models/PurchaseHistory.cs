using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHub.Models
{
	public class PurchaseHistory
	{
		[Key]
		[Column("ID")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }

		[Required]
		[ForeignKey("Ticket")]
		public int TicketId { get; set; }
		public Ticket Ticket { get; set; }

		[Required]
		[Column("date")]
		public DateTime Date { get; set; }
		[Required]
		[Column ("price")]
		public decimal Price { get; set; }
	}
}
