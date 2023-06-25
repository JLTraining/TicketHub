using System.ComponentModel;
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
		public Event? Event { get; set; }
		[Required]
		[ForeignKey("User")]
		public string? SellerId { get; set; }
		public ApplicationUser? Seller { get; set; }
		[Required]
		[Column("price")]
		public decimal Price { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
		[Column("quantity")]
		public int Quantity { get; set; }

		[Index("IX_Ticket_Row_Seat", 1, IsUnique = true)]
		[MaxLength(255)]
		public string? Row { get; set; }
		[Index("IX_Ticket_Row_Seat", 2, IsUnique = true)]
		[MaxLength(255)]
		public string? Seat { get; set; }

		public bool? isListed { get; set; }
    }
}
