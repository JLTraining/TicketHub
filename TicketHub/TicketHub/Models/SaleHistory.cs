using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHub.Models
{
	public class SaleHistory
	{
		[Key]
		[Column("ID")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[ForeignKey("User")]
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		[Required]
		[ForeignKey("Ticket")]
		public int TicketId { get; set; }
		public Ticket Ticket { get; set; }

		[Required]
		[Column("date")]
		public DateTime Date { get; set; }
		[Column("price")]
		public decimal Price { get; set; }
	}
}
