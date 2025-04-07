using MediatR;
using TodoList.Application.Common.Requests.User;
using TodoList.Application.Common.Responses;

namespace TodoList.Application.Features.User.ConfirmUserEmail;

public class ConfirmEmailCommand : UserEmailRequest, IRequest<BaseResponse>
{
    public ConfirmEmailCommand(UserEmailRequest request) : base(request)
    {
    }
}