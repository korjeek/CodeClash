using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeClash.Persistence.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(room => room.Id);

        builder
            .HasMany(r => r.Participants)
            .WithOne(u => u.Room)
            .HasForeignKey(u => u.RoomId);

        builder
            .HasOne(r => r.IssueEntity)
            .WithMany()
            .HasForeignKey(r => r.IssueId);
    }
}