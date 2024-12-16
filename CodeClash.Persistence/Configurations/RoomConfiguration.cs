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
            .HasOne<IssueEntity>()
            .WithMany()
            .HasForeignKey(r => r.IssueId);
        builder
            .HasMany<UserEntity>()
            .WithOne();
    }
}