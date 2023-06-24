namespace TicketHub.DataTransferObjects
{
    public class EditTicket
    {
        public int EventId { get; set; }
        public string? SellerId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Row { get; set; }
        public string? Seat { get; set; }
    }
}
