using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Messages.Error;
using TodoList.Application.Common.Messages.Success;
using TodoList.Application.Common.Responses;
using TodoList.Application.Interfaces.Services;

namespace TodoList.Application.Features.User.ConfirmUserEmail;

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, BaseResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IEmailService _emailService;
    
    public  ConfirmEmailHandler(UserManager<Domain.Entities.User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<BaseResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ValidationException(UserErrorMessages.EmailNotCorrect);
        
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException(UserErrorMessages.UserNotFound);

        string code = _emailService.GenerateRandomCode();

        await _userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
            
        await _emailService.SendEmailAsync(user.Email!, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        return new BaseResponse { IsSuccessfully = true };
    }
}