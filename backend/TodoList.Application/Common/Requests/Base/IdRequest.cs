namespace TodoList.Application.Common.Requests.Base;

public class IdRequest
{
    /// <summary>
    /// Id для запроса сущности
    /// </summary>
    public Guid Id { get; set; } 
    
    public IdRequest()
    {
    }

    public IdRequest(IdRequest requests)
    {
        Id = requests.Id;
    }
}