using MediatR;
using TodoList.Application.Common.Requests.Base;
using TodoList.Application.Common.Responses.User;

namespace TodoList.Application.Features.User.GetUserById;

public class GetUserByIdQuery : IdRequest, IRequest<UserInfoResponse>
{
    public GetUserByIdQuery(IdRequest request) : base(request)
    {
    }
}