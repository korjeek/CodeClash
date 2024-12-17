using CodeClash.Core.Models;
using CodeClash.Persistence.Configuration;
using CodeClash.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace CodeClash.Persistence;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<IssueEntity> Issues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());  
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new IssueConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}