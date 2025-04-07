using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Common.Requests.Auth;
using TodoList.Application.Features.Authorization.Auth;
using TodoList.Application.Features.Authorization.Login;
using TodoList.Application.Features.Authorization.Logout;

namespace TodoList.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="request">Запрос на регистрацию</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Auth")]
    public async Task<IActionResult> AuthUser(AuthRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AuthCommand(request), cancellationToken);

        if (result.IsSuccessfully)
        {
            Response.Cookies.Append("auth-cookie", result.Token, new CookieOptions
            {
                Path = "/",
                Expires = DateTimeOffset.Now.AddHours(2),
            });
            return Ok(result);
        }

        return NotFound(result);
    }

    /// <summary>
    /// Вход пользователя в аккаунт
    /// </summary>
    /// <param name="request">Запрос на вход в аккаунт</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginCommand(request), cancellationToken);

        if (result.IsSuccessfully)
        {
            Response.Cookies.Append("auth-cookie", result.Token, new CookieOptions
            {
                Path = "/",
                Expires = DateTimeOffset.Now.AddHours(2),
            });
            return Ok(result);
        }

        return NotFound(result);
    }
    
    /// <summary>
    /// Выход из аккаунта
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LogoutCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            Response.Cookies.Delete("auth-cookie");

        return Ok();
    }
}