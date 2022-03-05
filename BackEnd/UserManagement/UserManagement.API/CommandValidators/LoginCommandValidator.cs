using FluentValidation;
using UserManagement.API.Commands;

namespace UserManagement.API.CommandValidators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(command => command).Must(command => NullAndEmptyFieldValidations(command));
        }


        private bool NullAndEmptyFieldValidations(LoginCommand command)
        {
            if (String.IsNullOrEmpty(command.Email))
                throw new Exception("Email cannot be empty");

            if (String.IsNullOrEmpty(command.Password))
                throw new Exception("Password cannot be empty");

            return true;
        }
    }
}
