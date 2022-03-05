using MediatR;
using Ship.API.ApiModels.Response;
using Ship.API.Queries;
using Ship.Core.Entities;
using Ship.Core.IRepositories;
using System.Linq.Expressions;
using System.Linq;
using Ship.API.ApiModels;
using AutoMapper;

namespace Ship.API.QueryHandler
{
    public class GetShipListingQueryHandler : IRequestHandler<GetShipListingQuery, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShipRepository _shipRepository;
        public GetShipListingQueryHandler(IMapper mapper, IShipRepository shipRepository)
        {
            _mapper = mapper;
            _shipRepository = shipRepository;
        }

        public async Task<ApiResponse> Handle(GetShipListingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<ShipEntity, bool>> predicate = x => !x.IsDeleted;


                var response = await _shipRepository.GetList(predicate, request.PageIndex, request.PageSize);


                if (response.Item2 > 0)
                {
                    var ships = _mapper.Map<List<ShipApiModel>>(response.Item1);

                    return ApiResponse.CreatePaginationResponse("Success", ships, response.Item2, request.PageIndex, request.PageSize);
                }

                else
                    return ApiResponse.CreatePaginationResponse("Success", new List<ShipApiModel>(), response.Item2, request.PageIndex, request.PageSize);


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
