using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ParksLookup.Models
{
    public class ParksLookupContext : IdentityDbContext
    {
        public ParksLookupContext(DbContextOptions<ParksLookupContext> options)
            : base(options)
        {
        }
          protected override void OnModelCreating(ModelBuilder builder)
        {
          base.OnModelCreating(builder); //helps with configuration 
          builder.Entity<Park>()
              .HasData(
                  new Park { ParkId = 1, Name = "Mt. Hood", State = "Oregon", Type = "National Park" },
                  new Park { ParkId = 2, Name = "Molalla River", State = "Oregon", Type = "State Park" },
                  new Park { ParkId = 3, Name = "Glacier", State = "Montana", Type = "National Park" }

              );
        }

        public DbSet<Park> Parks { get; set; }
    }
}