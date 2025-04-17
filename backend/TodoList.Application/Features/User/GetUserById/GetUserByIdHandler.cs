using AutoMapper;
using MediatR;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Responses.User;
using TodoList.Application.Interfaces.Repositories;

namespace TodoList.Application.Features.User.GetUserById;

public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, UserInfoResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<UserInfoResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new ValidationException();

        var user = await _userRepository.FindById(request.Id, cancellationToken);

        if (user is null || user.UserInfo is null)
            throw new NotFoundException(typeof(Domain.Entities.User));
        
        return _mapper.Map<UserInfoResponse>(user);
    }
}