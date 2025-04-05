namespace TodoList.Application.Common.Exceptions;

public class ValidationException : BaseException
{
    public ValidationException() : base("Validation Failed.")
    {
    }
}