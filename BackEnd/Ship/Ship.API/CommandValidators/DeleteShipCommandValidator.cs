using FluentValidation;
using Ship.API.Commands;

namespace Ship.API.CommandValidators
{

    public class DeleteShipCommandValidator : AbstractValidator<DeleteShipCommand>
    {
        public DeleteShipCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
