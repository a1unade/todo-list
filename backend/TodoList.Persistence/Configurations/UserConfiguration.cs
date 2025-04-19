using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Persistence.Configurations;

/// <summary>
/// Конфигурация для пользователя в БД
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(u => u.Nickname)
            .IsRequired()
            .HasMaxLength(60);

        builder.HasMany(u => u.Projects)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        builder.HasOne(u => u.AvatarUrl)
            .WithMany()
            .HasForeignKey(u => u.AvatarId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(u => u.UserInfo)
            .WithOne()
            .HasForeignKey<User>(u => u.UserInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}