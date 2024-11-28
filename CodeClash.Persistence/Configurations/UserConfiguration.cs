using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeClash.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder
            .HasOne(u => u.Room)
            .WithMany(r => r.Participants)
            .HasForeignKey(u => u.RoomId)
            .OnDelete(DeleteBehavior.SetNull); // при удалении комнаты Room обнуляется
    }
}