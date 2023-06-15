namespace TicketHub.Models
{
	public class Ticket
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public int SellerId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
