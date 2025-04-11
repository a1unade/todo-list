using MediatR;
using TodoList.Application.Common.Requests.Auth;
using TodoList.Application.Common.Responses.Auth;
using TodoList.Application.Interfaces.Commands;

namespace TodoList.Application.Features.Authorization.Login;

public class LoginCommand : LoginRequest, IRequest<AuthResponse>
{
    public LoginCommand(LoginRequest request) : base(request)
    {
        
    }
}