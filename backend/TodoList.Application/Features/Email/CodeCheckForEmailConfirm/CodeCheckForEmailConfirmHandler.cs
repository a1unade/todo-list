using MediatR;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Messages.Error;
using TodoList.Application.Common.Messages.Success;
using TodoList.Application.Common.Responses;
using TodoList.Application.Interfaces.Services;

namespace TodoList.Application.Features.Email.CodeCheckForEmailConfirm;

public class CodeCheckForEmailConfirmHandler : IRequestHandler<CodeCheckForEmailConfirmCommand, BaseResponse>
{
    private readonly IEmailService _emailService;
    private readonly UserManager<Domain.Entities.User> _userManager;

    public CodeCheckForEmailConfirmHandler(IEmailService emailService, UserManager<Domain.Entities.User> userManager
    )
    {
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<BaseResponse> Handle(CodeCheckForEmailConfirmCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Email))
            throw new ValidationException();

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException(UserErrorMessages.UserNotFound);

        var claims = await _userManager.GetClaimsAsync(user);

        if (!claims.Any())
            throw new BadRequestException(AnyErrorMessages.ClaimsIsEmpty);

        var claim = claims.FirstOrDefault(c =>
            c.Type == EmailSuccessMessage.EmailConfirmCodeString && c.Value == request.Code);

        if (claim == null)
            throw new BadRequestException(AnyErrorMessages.InvalidConfirmationCode);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        await _userManager.ConfirmEmailAsync(user, code);

        await _userManager.RemoveClaimAsync(user, claim);

        await _emailService.SendEmailAsync(user.Email!,
            EmailSuccessMessage.EmailThankYouMessage,
            EmailSuccessMessage.EmailConfirmCodeSuccess);

        return new BaseResponse { IsSuccessfully = true, Message = EmailSuccessMessage.EmailConfirmCodeSuccess};
    }
}