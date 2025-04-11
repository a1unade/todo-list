using MediatR;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Messages.Error;
using TodoList.Application.Common.Responses;
using TodoList.Application.Interfaces.Repositories;

namespace TodoList.Application.Features.Authorization.Logout;

public class LogoutHandler : IRequestHandler<LogoutCommand, BaseResponse>
{
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IUserRepository _repository;

    public LogoutHandler(SignInManager<Domain.Entities.User> signInManager, IUserRepository repository)
    {
        _signInManager = signInManager;
        _repository = repository;
    }
    
    public async Task<BaseResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindById(request.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException(UserErrorMessages.UserNotFound);

        await _signInManager.SignOutAsync();
        
        return new BaseResponse { IsSuccessfully = true };
    }
}