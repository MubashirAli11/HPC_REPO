using MediatR;
using Ship.API.ApiModels.Response;
using Ship.API.Queries;
using Ship.Core.Entities;
using Ship.Core.IRepositories;
using System.Linq.Expressions;

namespace Ship.API.QueryHandler
{
    public class GetShipListingQueryHandler : IRequestHandler<GetShipListingQuery, ApiResponse>
    {
        private readonly IShipRepository _shipRepository;
        public GetShipListingQueryHandler(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public async Task<ApiResponse> Handle(GetShipListingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<ShipEntity, bool>> predicate = x => !x.IsDeleted;


                var response = await _shipRepository.GetList(predicate, request.PageIndex, request.PageSize);


                if (response.Item2 > 0)
                    return ApiResponse.CreatePaginationResponse("Success", response.Item1, response.Item2, request.PageIndex, request.PageSize);

                else
                    return ApiResponse.CreateFailedResponse("No data found");
              

            }
            catch (Exception exp)
            {
                //TODO:
                //Add exceptions
                return ApiResponse.CreateFailedResponse(exp.Message);
            }
        }
    }
}
