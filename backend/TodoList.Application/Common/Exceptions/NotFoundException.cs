using TodoList.Domain.Entities;
using File = System.IO.File;

namespace TodoList.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    private static readonly IDictionary<Type, string> EntityException = new Dictionary<Type, string>
    {
        [typeof(File)] = "Файл не найден",
        [typeof(User)] = "Пользователь не найден",
        [typeof(UserInfo)] = "Информация о пользователе не найдена",
    };

    public NotFoundException(Guid id)
        : base($"Сущность с идентификатором {id} не найдена.")
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(Type entityType)
        : base(ExceptionEntity(entityType))
    {
    }

    public NotFoundException()
        : base("Entity not found.`")
    {
    }

    private static string ExceptionEntity(Type entityType) =>
        EntityException.TryGetValue(entityType, out var text) ? text : $"{entityType.FullName} не найдена.";
}