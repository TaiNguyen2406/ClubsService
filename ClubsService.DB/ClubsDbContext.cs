using ClubsService.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubsService.DB
{
    public partial class ClubsDbContext : DbContext
    {
        public ClubsDbContext(DbContextOptions<ClubsDbContext> options)
            : base(options)
        {
          
        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Club>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }

    }
}