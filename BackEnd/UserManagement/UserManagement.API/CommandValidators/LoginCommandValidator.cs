using FluentValidation;
using UserManagement.API.Commands;

namespace UserManagement.API.CommandValidators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(command => command.Email).NotEmpty();
            RuleFor(command => command.Password).NotEmpty();
        }
    }
}
