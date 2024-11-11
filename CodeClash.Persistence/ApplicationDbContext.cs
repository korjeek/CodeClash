using CodeClash.Core.Models;
using CodeClash.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CodeClash.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}