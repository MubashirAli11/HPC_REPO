using MediatR;
using Ship.API.ApiModels.Response;

namespace Ship.API.Commands
{
    public class CreateShipCommand : IRequest<ApiResponse>
    {
        public string Name { get; private set; }
        public double Length { get; private set; }
        public double Width { get; private set; }
        public string Code { get; private set; }
    }
}
