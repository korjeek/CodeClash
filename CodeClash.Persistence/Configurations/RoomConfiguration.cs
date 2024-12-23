using CodeClash.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeClash.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(room => room.Id);
        builder
            .HasOne<IssueEntity>()
            .WithMany()
            .HasForeignKey(r => r.IssueId);
    }
}