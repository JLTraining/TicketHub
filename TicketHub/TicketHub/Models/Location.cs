using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TicketHub.Models
{
	public class Location
	{
		[Key]
		[Column("ID")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[Column("street")]
		[StringLength(150)]
		public string Street { get; set; }
		[Required]
		[Column("city")]
		[StringLength(50)]
		public string City { get; set; }
		[Required]
		[Column("state")]
		[StringLength(100)]
		public string State { get; set; }
		[Required]
		[Column("country")]
		[StringLength(50)]
		public string Country { get; set; }
		[Column("postal_code")]
		[StringLength(10)]
		public string PostalCode { get; set; }
		
		[Column("building_name")]
		[StringLength(150)]
		public string? BuildingName { get; set; }

		public ICollection<EventLocation> EventLocations { get; set; }//Probably should make many-to-many entity instead of this
	}
}
