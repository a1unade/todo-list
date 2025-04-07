using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Common.Requests.Base;
using TodoList.Application.Common.Requests.Email;
using TodoList.Application.Common.Requests.User;
using TodoList.Application.Features.Email.CodeCheckForEmailConfirm;
using TodoList.Application.Features.User.CheckUserEmail;
using TodoList.Application.Features.User.ConfirmUserEmail;
using TodoList.Application.Features.User.GetUserById;

namespace TodoList.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Проверка кода для подтверждения почты
    /// </summary>
    /// <param name="request">Код и Id пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("CodeCheckForEmail")]
    public async Task<IActionResult> CodeCheckForEmail(CodeCheckRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CodeCheckForEmailConfirmCommand(request), cancellationToken);
        if (result.IsSuccessfully)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    /// <summary>
    /// Запрос на отправку сообщения с кодом подтверждения
    /// </summary>
    /// <param name="request">Почта пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(UserEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ConfirmEmailCommand(request), cancellationToken);

        if (result.IsSuccessfully)
            return Ok(result);

        return BadRequest(result);
    }
     
    /// <summary>
    /// Проверка почты юзера
    /// </summary>
    /// <param name="request">Почта пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("CheckUserEmail")]
    public async Task<IActionResult> CheckUserEmail(UserEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CheckUserEmailCommand(request), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить информацию о пользователе
    /// </summary>
    /// <param name="idRequest">Id пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о пользователе</returns>
    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById([FromQuery] IdRequest idRequest, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserByIdQuery(idRequest), cancellationToken);

        if (response.IsSuccessfully)
            return Ok(response);

        return Forbid();
    }
}