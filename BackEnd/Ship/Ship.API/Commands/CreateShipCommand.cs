using MediatR;
using Ship.API.ApiModels.Response;

namespace Ship.API.Commands
{
    public class CreateShipCommand : IRequest<ApiResponse>
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public string Code { get; set; }
    }
}
