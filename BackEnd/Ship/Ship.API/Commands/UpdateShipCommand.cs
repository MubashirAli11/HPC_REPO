using MediatR;
using Ship.API.ApiModels.Response;

namespace Ship.API.Commands
{
    public class UpdateShipCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
    }
}
