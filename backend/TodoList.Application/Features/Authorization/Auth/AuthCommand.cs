using MediatR;
using TodoList.Application.Common.Requests.Auth;
using TodoList.Application.Common.Responses.Auth;

namespace TodoList.Application.Features.Authorization.Auth;

public class AuthCommand : AuthRequest, IRequest<AuthResponse>
{
    public AuthCommand(AuthRequest request) : base(request)
    {
    }
}