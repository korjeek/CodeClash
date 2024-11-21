using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeClash.Persistence.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(room => room.Id);
        builder.HasOne(r => r.Admin)
            .WithMany()
            .HasForeignKey(r => r.AdminId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // builder.HasOne(room => room.Admin)
        //     .WithOne()
        //     .OnDelete(DeleteBehavior.Cascade); // При удалении Admin удаляется комната
        builder.HasIndex(r => r.AdminId).IsUnique();
    }
}