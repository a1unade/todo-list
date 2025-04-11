using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Persistence.Configurations;

/// <summary>
/// Конфигурация для тегов в БД
/// </summary>
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Text)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(t => t.Color)
            .HasMaxLength(7);
    }
}
