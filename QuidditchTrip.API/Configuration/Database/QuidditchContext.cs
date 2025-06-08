using Microsoft.EntityFrameworkCore;
using QuidditchTrip.Models;

namespace QuidditchTrip.API.Configuration.Database;

public class QuidditchContext : DbContext
{
    public QuidditchContext(DbContextOptions<QuidditchContext> options) : base(options)
    { }

    public DbSet<Game> Games { get; set; }
    public DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
            .HasKey(t => t.TeamKey);

        modelBuilder.Entity<Game>()
            .HasKey(g => g.GameKey);

        modelBuilder.Entity<Team>()
            .HasOne(e => e.Game)
            .WithMany(e => e.Teams)
            .HasForeignKey(t => t.GameKey)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Ignore<LeaderboardEntry>();
    }
}
