using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketHub.Models;

namespace TicketHub.Data
{
    public class TicketHubContext : DbContext
    {
        public TicketHubContext (DbContextOptions<TicketHubContext> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EventLocation>()
                .HasKey(c => new {c.EventId, c.LocationId});

			modelBuilder.Entity<PurchaseHistory>()
			.HasOne(p => p.User)
			.WithMany(u => u.PurchaseHistories)
			.HasForeignKey(p => p.UserId)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<SaleHistory>()
			.HasOne(p => p.User)
			.WithMany(u => u.SaleHistories)
			.HasForeignKey(p => p.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		}

		public DbSet<TicketHub.Models.User> User { get; set; } = default!;

        public DbSet<TicketHub.Models.Event> Event { get; set; } = default!;

        public DbSet<TicketHub.Models.Location> Location { get; set; } = default!;

        public DbSet<TicketHub.Models.EventLocation> EventLocation { get; set; } = default!;

        public DbSet<TicketHub.Models.Ticket> Ticket { get; set; } = default!;

        public DbSet<TicketHub.Models.PurchaseHistory> PurchaseHistory { get; set; } = default!;

        public DbSet<TicketHub.Models.SaleHistory> SaleHistory { get; set; } = default!;
    }
}
