using MediatR;
using TodoList.Application.Common.Requests.Email;
using TodoList.Application.Common.Responses;

namespace TodoList.Application.Features.Email.CodeCheckForEmailConfirm;

public class CodeCheckForEmailConfirmCommand : CodeCheckRequest, IRequest<BaseResponse>
{
    public CodeCheckForEmailConfirmCommand(CodeCheckRequest request) : base(request)
    {
    }
}