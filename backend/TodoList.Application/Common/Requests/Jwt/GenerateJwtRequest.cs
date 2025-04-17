namespace TodoList.Application.Common.Requests.Jwt;

public class GenerateJwtRequest
{
    public int Step { get; set; }

    public Dictionary<string, object> Data { get; set; } = new();
    
    public GenerateJwtRequest()
    {
    }

    public GenerateJwtRequest(GenerateJwtRequest request)
    {
        Step = request.Step;
        Data = request.Data;
    }
}