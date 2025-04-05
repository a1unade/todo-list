using System.Net;

namespace TodoList.Application.Common.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base (message, HttpStatusCode.BadRequest)
    {
    }
}