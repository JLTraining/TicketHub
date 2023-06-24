using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TicketHub.Models;

namespace TicketHub.DataTransferObjects
{
    public class CreateEvent
    {
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}
