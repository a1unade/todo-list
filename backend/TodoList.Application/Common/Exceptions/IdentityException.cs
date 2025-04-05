using System.Net;

namespace TodoList.Application.Common.Exceptions;

public class IdentityException : BaseException
{
    public IdentityException(string message) : base(message)
    {
    }

    public IdentityException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}