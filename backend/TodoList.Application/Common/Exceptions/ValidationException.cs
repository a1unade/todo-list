namespace TodoList.Application.Common.Exceptions;

public class ValidationException : BaseException
{
    public ValidationException(string message) : base(message)
    {
    }
    
    public ValidationException() : base("Validation Failed.")
    {
    }
}