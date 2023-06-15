using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TicketHub.Models
{
	public class Ticket
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("ID")]
		public int Id { get; set; }
		[Required]
		[ForeignKey("Event")]
		public int EventId { get; set; }
		public Event Event { get; set; }
		[Required]
		[ForeignKey("User")]
		public int SellerId { get; set; }
		public User Seller { get; set; }
		[Required]
		[Column("price")]
		public decimal Price { get; set; }
		[Required]
		[Column("quantity")]
		public int Quantity { get; set; }
	}
}
