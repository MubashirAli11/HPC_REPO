using FluentValidation;
using Ship.API.Commands;

namespace Ship.API.CommandValidators
{
    public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
    {
        public CreateShipCommandValidator()
        {
            RuleFor(command => command.Code).NotEmpty();
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Length).NotEmpty();
            RuleFor(command => command.Width).NotEmpty();
        }
    }
}
