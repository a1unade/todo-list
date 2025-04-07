using MediatR;
using TodoList.Application.Common.Requests.User;
using TodoList.Application.Common.Responses.User;

namespace TodoList.Application.Features.User.CheckUserEmail;

public class CheckUserEmailCommand : UserEmailRequest, IRequest<UserEmailResponse>
{
    public CheckUserEmailCommand(UserEmailRequest request) : base(request)
    {
    }
}