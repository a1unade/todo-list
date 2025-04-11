using FluentValidation;
using TodoList.Application.Interfaces.Commands;

namespace TodoList.Application.Validators;

public class AuthValidator<T> : AbstractValidator<T> where T : IAuthCommand
{
    public AuthValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email указан в неверном формате.");

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithMessage("Пароль должен содержать минимум 6 символов.");
    }
}