using MediatR;
using Ship.API.ApiModels.Response;

namespace Ship.API.Queries
{
    public class GetShipListingQuery : IRequest<ApiResponse>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
