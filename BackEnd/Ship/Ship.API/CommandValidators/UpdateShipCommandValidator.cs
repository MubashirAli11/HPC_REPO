using FluentValidation;
using Ship.API.Commands;

namespace Ship.API.CommandValidators
{

    public class UpdateShipCommandValidator : AbstractValidator<UpdateShipCommand>
    {
        public UpdateShipCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.Code).NotEmpty();
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Length).NotEmpty();
            RuleFor(command => command.Width).NotEmpty();
        }
    }
}
