using FluentValidation;
using Ship.API.Commands;
using System.Text.RegularExpressions;

namespace Ship.API.CommandValidators
{

    public class UpdateShipCommandValidator : AbstractValidator<UpdateShipCommand>
    {
        public UpdateShipCommandValidator()
        {

            RuleFor(command => command.Code).NotEmpty();
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Length).NotEmpty();
            RuleFor(command => command.Width).NotEmpty();

            RuleFor(command => command).Must(command => ValidateCodeFormat(command))
             .WithMessage("Code should follow this format AAAA-1111-A1 A: Alphabets 1: 0-9");
        }


        private bool ValidateCodeFormat(UpdateShipCommand command)
        {
            Regex r = new Regex(@"^[A-Za-z]{4}-[0-9]{4}-[A-Za-z]{1}[0-9]{1}$");

            if (r.Match(command.Code).Success)
            {
                return true;
            }

            return false;
        }
    }
}
