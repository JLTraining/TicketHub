namespace TicketHub.Models
{
	public class SaleHistory
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int TicketId { get; set; }
		public DateTime Date { get; set; }
		public decimal Price { get; set; }
	}
}
