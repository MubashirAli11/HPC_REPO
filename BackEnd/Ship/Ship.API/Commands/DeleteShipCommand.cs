using MediatR;
using Ship.API.ApiModels.Response;

namespace Ship.API.Commands
{
    public class DeleteShipCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
