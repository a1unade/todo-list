using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Messages.Error;
using TodoList.Application.Common.Messages.Success;
using TodoList.Application.Common.Responses.User;
using TodoList.Application.Interfaces.Services;

namespace TodoList.Application.Features.User.CheckUserEmail;

public class CheckUserEmailHandler : IRequestHandler<CheckUserEmailCommand, UserEmailResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IEmailService _emailService;

    public CheckUserEmailHandler(IEmailService emailService, UserManager<Domain.Entities.User> userManager)
    {
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<UserEmailResponse> Handle(CheckUserEmailCommand request, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(request.Email))
            throw new ValidationException(UserErrorMessages.EmailNotCorrect);

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new UserEmailResponse { NewUser = true, IsSuccessfully = true };

        if (user.EmailConfirmed)
            return new UserEmailResponse
            {
                IsSuccessfully = true,
                NewUser = false,
                Confirmation = true
            };
        
        var code = _emailService.GenerateRandomCode();
        await _userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
        await _emailService.SendEmailAsync(request.Email, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        return new UserEmailResponse { IsSuccessfully = true };
    }
}