using System.Net;
using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Messages.Error;
using TodoList.Application.Common.Messages.Success;
using TodoList.Application.Common.Responses.Auth;
using TodoList.Application.Interfaces.Repositories;
using TodoList.Application.Interfaces.Services;

namespace TodoList.Application.Features.Authorization.Auth;

public class AuthHandler(
    UserManager<Domain.Entities.User> userManager,
    SignInManager<Domain.Entities.User> signInManager,
    IUserRepository userRepository,
    IJwtGenerator jwtGenerator,
    IEmailService emailService,
    IMapper mapper)
    : IRequestHandler<AuthCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmail(request.Email, cancellationToken);

        if (user is not null)
            throw new BadRequestException(AuthErrorMessages.EmailIsBusy);

        user = mapper.Map<Domain.Entities.User>(request);

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new IdentityException(errors, HttpStatusCode.BadRequest);
        }

        await userManager.AddToRoleAsync(user, "User");
        
        await signInManager.SignInAsync(user, false);

        var code = emailService.GenerateRandomCode();
        await userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
        await emailService.SendEmailAsync(user.Email!, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        var jwtToken = await jwtGenerator.GenerateToken(user); 
        var authResponse = mapper.Map<AuthResponse>(user);
        authResponse.Token = jwtToken;
        
        return authResponse;
    }
}