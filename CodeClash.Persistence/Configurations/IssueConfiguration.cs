using CodeClash.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeClash.Persistence.Configurations;

public class IssueConfiguration : IEntityTypeConfiguration<IssueEntity>
{
    public void Configure(EntityTypeBuilder<IssueEntity> builder)
    {
        builder.HasKey(issue => issue.Id);
    }
}