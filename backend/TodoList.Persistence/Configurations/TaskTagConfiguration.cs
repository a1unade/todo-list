using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Persistence.Configurations;

/// <summary>
/// Конфигурация для связи многие-ко-многим у задач и тегов в БД
/// </summary>
public class TaskTagConfiguration : IEntityTypeConfiguration<TaskTag>
{
    public void Configure(EntityTypeBuilder<TaskTag> builder)
    {
        builder.HasKey(tt => new { tt.TaskId, tt.TagId });

        builder.HasOne(tt => tt.Task)
            .WithMany(t => t.Tags)
            .HasForeignKey(tt => tt.TaskId);

        builder.HasOne(tt => tt.Tag)
            .WithMany(t => t.TaskTags)
            .HasForeignKey(tt => tt.TagId);
    }
}