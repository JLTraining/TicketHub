namespace TicketHub.DataTransferObjects
{
    public class OrderTicket
    {
        public int EventId { get; set; }
        public string? BuyerId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Row { get; set; }
        public string? Seat { get; set; }
    }
}
