using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketHub.Models;

namespace TicketHub.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventLocation>()
            .HasKey(c => new { c.EventId, c.LocationId });

        modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasNoKey();

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasNoKey();

        modelBuilder.Entity<IdentityUserToken<string>>()
            .HasNoKey();

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

    public DbSet<TicketHub.Models.ApplicationUser> User { get; set; } = default!;

    public DbSet<TicketHub.Models.Event> Event { get; set; } = default!;

    public DbSet<TicketHub.Models.Location> Location { get; set; } = default!;

    public DbSet<TicketHub.Models.EventLocation> EventLocation { get; set; } = default!;

    public DbSet<TicketHub.Models.Ticket> Ticket { get; set; } = default!;

    public DbSet<TicketHub.Models.PurchaseHistory> PurchaseHistory { get; set; } = default!;

    public DbSet<TicketHub.Models.SaleHistory> SaleHistory { get; set; } = default!;

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);
        }

    }
}

