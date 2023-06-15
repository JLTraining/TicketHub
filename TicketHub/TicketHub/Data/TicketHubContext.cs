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

        public DbSet<TicketHub.Models.User> User { get; set; } = default!;

        public DbSet<TicketHub.Models.Event> Event { get; set; } = default!;
    }
}
