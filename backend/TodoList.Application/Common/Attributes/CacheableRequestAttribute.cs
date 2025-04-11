namespace TodoList.Application.Common.Attributes;

/// <summary>
/// Атрибут для меток на кэшируемые запросы
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CacheableRequestAttribute : Attribute
{
    public int ExpirationInSeconds { get; set; }

    public CacheableRequestAttribute(int expirationInSeconds)
    {
        ExpirationInSeconds = expirationInSeconds;
    }
}