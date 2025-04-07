using MediatR;
using TodoList.Application.Common.Requests.Auth;
using TodoList.Application.Common.Responses.Auth;

namespace TodoList.Application.Features.Authorization.Login;

public class LoginCommand : LoginRequest, IRequest<AuthResponse>
{
    public LoginCommand(LoginRequest request) : base(request)
    {
        
    }
}