using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Common.Messages.Error;
using TodoList.Application.Common.Messages.Success;
using TodoList.Application.Common.Responses.Auth;
using TodoList.Application.Interfaces.Repositories;
using TodoList.Application.Interfaces.Services;
using TodoList.Domain.Entities;

namespace TodoList.Application.Features.Authorization.Auth;

public class AuthHandler : IRequestHandler<AuthCommand, AuthResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IEmailService _emailService;

    public AuthHandler(UserManager<Domain.Entities.User> userManager,
        SignInManager<Domain.Entities.User> signInManager,
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator,
        IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _emailService = emailService;
    }

    public async Task<AuthResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password) || 
            String.IsNullOrEmpty(request.Gender) || String.IsNullOrEmpty(request.Surname) ||
            String.IsNullOrEmpty(request.Name))
            throw new ValidationException();

        var user = await _userRepository.FindByEmail(request.Email, cancellationToken);

        if (user is not null)
            throw new BadRequestException(AuthErrorMessages.EmailIsBusy);

        user = new Domain.Entities.User
        {
            Nickname = request.Name + " " + request.Surname,
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = false
        };
        
        var userInfo = new UserInfo
        {
            Name = request.Name,
            Surname = request.Surname,
            BirthDate = DateOnly.FromDateTime(request.DateOfBirth),
            Gender = request.Gender,
            Country = request.Country
        };

        user.UserInfo = userInfo;

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new IdentityException(errors, HttpStatusCode.BadRequest);
        }

        await _userManager.AddToRoleAsync(user, "User");
        
        await _signInManager.SignInAsync(user, false);

        var code = _emailService.GenerateRandomCode();
        await _userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
        
        await _emailService.SendEmailAsync(user.Email, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        var jwtToken = await _jwtGenerator.GenerateToken(user); 

        return new AuthResponse
        {
            IsSuccessfully = true,
            Token = jwtToken,
            UserId = user.Id
        };
    }
}