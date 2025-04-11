using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Messages.Error;
using TodoList.Application.Common.Messages.Success;
using TodoList.Application.Common.Responses.User;
using TodoList.Application.Interfaces.Services;

namespace TodoList.Application.Features.User.CheckUserEmail;

public class CheckUserEmailHandler(IEmailService emailService, UserManager<Domain.Entities.User> userManager)
    : IRequestHandler<CheckUserEmailCommand, UserEmailResponse>
{
    public async Task<UserEmailResponse> Handle(CheckUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new UserEmailResponse { NewUser = true, IsSuccessfully = true };

        if (user.EmailConfirmed)
            return new UserEmailResponse
            {
                IsSuccessfully = true,
                NewUser = false,
                Confirmation = true
            };
        
        var code = emailService.GenerateRandomCode();
        await userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
        await emailService.SendEmailAsync(request.Email, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        return new UserEmailResponse { IsSuccessfully = true };
    }
}