using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TicketHub.Models
{
	public class Event
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("ID")]
		public int Id { get; set; }
		[Required]
		[Column("title")]
		[StringLength(50)]
		public string? Title { get; set; }
		[Required]
		[Column("date")]
		public DateTime Date { get; set; }
		[Column("description")]
		[StringLength(255)]
		public string? Description { get; set; }

		public ICollection<EventLocation>? EventLocations { get; set; }
	}
}
