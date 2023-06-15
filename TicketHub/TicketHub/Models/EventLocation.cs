using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHub.Models
{
	public class EventLocation
	{
		[Key, Column(Order = 0)]
		[ForeignKey("Event")]
		public int EventId { get; set; }
		public Event Event { get; set; }

		[Key, Column(Order = 1)]
		[ForeignKey("Location")]
		public int LocationId { get; set; }
		public Location Location { get; set; }
	}
}
