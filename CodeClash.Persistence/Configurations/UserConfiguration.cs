using CodeClash.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeClash.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder
            .HasOne(u => u.Room)
            .WithMany(r => r.Participants)
            .HasForeignKey(u => u.RoomId)
            .OnDelete(DeleteBehavior.SetNull);
        // builder
        //     .HasOne(user => user.Room)
        //     .WithMany(room => room.Participants)
        //     .OnDelete(DeleteBehavior.SetNull); // при удалении комнаты RoomId обнуляется
        builder.HasIndex(user => new { user.RoomId, user.IsAdmin }); // индекс для ускоренного поиска КАК? пока непонятно немного...
    }
}