using MediatR;
using Ship.API.ApiModels.Response;

namespace Ship.API.Queries
{
    public class GetShipListingQuery : IRequest<ApiResponse>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
