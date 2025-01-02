namespace PlayFieldBuddy.Repositories;

using Microsoft.EntityFrameworkCore;
using PlayFieldBuddy.Domain.Models;

public class PlayFieldBuddyDbContext : DbContext
{

    public PlayFieldBuddyDbContext(DbContextOptions<PlayFieldBuddyDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Pitch> Pitches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.JoinedGames)
            .WithMany(e => e.Users);

        modelBuilder.Entity<Pitch>()
            .HasMany(e => e.Games)
            .WithOne(e => e.Pitch);

        modelBuilder.Entity<Game>()
            .HasOne(g => g.Owner)
            .WithMany(e => e.OwnedGames);
    }
}