using MediatR;
using TodoList.Application.Common.Requests.Auth;
using TodoList.Application.Common.Responses;

namespace TodoList.Application.Features.Authorization.Logout;

public class LogoutCommand : LogoutRequest, IRequest<BaseResponse>
{
    public LogoutCommand(LogoutRequest request) : base(request)
    {
    }
}