using MediatR;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Responses.User;
using TodoList.Application.Interfaces.Repositories;

namespace TodoList.Application.Features.User.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserInfoResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserInfoResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new ValidationException();

        var user = await _userRepository.FindById(request.Id, cancellationToken);

        if (user == null || user.UserInfo == null)
            throw new NotFoundException(typeof(Domain.Entities.User));
        
        return new UserInfoResponse
        {
            IsSuccessfully = true,
            Name = user.UserInfo.Name,
            Surname = user.UserInfo.Surname!,
            Email = user.Email!,
            Nickname = user.Nickname,
        };
    }
}