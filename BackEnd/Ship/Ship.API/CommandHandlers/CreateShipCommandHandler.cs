using MediatR;
using Ship.API.ApiModels.Response;
using Ship.API.Commands;
using Ship.Core.Entities;
using Ship.Core.IRepositories;

namespace Ship.API.CommandHandlers
{
    public class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateShipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> Handle(CreateShipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _unitOfWork.ShipRepository.Exists(request.Code))
                {
                    var ship = new ShipEntity(request.Name, request.Length, request.Width, request.Code);

                    _unitOfWork.ShipRepository.Add(ship);

                    var isAdded = await _unitOfWork.SaveChangesAsync();

                    if (isAdded)
                        return ApiResponse.CreateSuccessResponse("Success", isAdded);

                    return ApiResponse.CreateFailedResponse("Unable to add ship record");
                }
                else
                    return ApiResponse.CreateFailedResponse("Record with same code already exists");

            }
            catch (Exception exp)
            {
                return ApiResponse.CreateFailedResponse(exp.Message);
            }
        }
    }
}
