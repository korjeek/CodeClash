using CodeClash.Core.Models;
using CodeClash.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    // public DbSet<Room> Rooms { get; set; }
    // public DbSet<Issue> Issues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}