using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Messages.Error;
using TodoList.Application.Common.Messages.Success;
using TodoList.Application.Common.Responses;
using TodoList.Application.Interfaces.Services;

namespace TodoList.Application.Features.User.ConfirmUserEmail;

public class ConfirmEmailHandler(UserManager<Domain.Entities.User> userManager, IEmailService emailService)
    : IRequestHandler<ConfirmEmailCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException(UserErrorMessages.UserNotFound);

        string code = emailService.GenerateRandomCode();

        await userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
            
        await emailService.SendEmailAsync(user.Email!, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        return new BaseResponse { IsSuccessfully = true };
    }
}