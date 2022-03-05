using FluentValidation;
using Ship.API.ApiModels.Response;
using Ship.API.Commands;
using System.Text.RegularExpressions;

namespace Ship.API.CommandValidators
{
    public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
    {
        public CreateShipCommandValidator()
        {
            RuleFor(command => command).Must(command => NullAndEmptyFieldValidations(command));

            RuleFor(command => command).Must(command => ValidateCodeFormat(command))
                
                .WithMessage("Code should follow this format AAAA-1111-A1 A: Alphabets 1: 0-9");
        }

        private bool NullAndEmptyFieldValidations(CreateShipCommand command)
        {
            if (String.IsNullOrEmpty(command.Code))
                throw new Exception("Code cannot be empty");

            if (String.IsNullOrEmpty(command.Name))
                throw new Exception("Name cannot be empty");

            if (command.Length <= 0)
                throw new Exception("Length cannot be 0 or negative");

            if (command.Width <= 0)
                throw new Exception("Width cannot be 0 or negative");


            return true;
        }

        private bool ValidateCodeFormat(CreateShipCommand command)
        {
            Regex r = new Regex(@"^[A-Za-z]{4}-[0-9]{4}-[A-Za-z]{1}[0-9]{1}$");

            if (r.Match(command.Code).Success)
            {
                return true;
            }

            throw new Exception("Code should follow this format AAAA-1111-A1 A: Alphabets 1: 0-9");

        }
    }
}
