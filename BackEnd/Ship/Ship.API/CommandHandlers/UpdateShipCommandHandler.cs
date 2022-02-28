using MediatR;
using Ship.API.ApiModels.Response;
using Ship.API.Commands;
using Ship.Core.Entities;
using Ship.Core.IRepositories;

namespace Ship.API.CommandHandlers
{
    public class UpdateShipCommandHandler : IRequestHandler<UpdateShipCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateShipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> Handle(UpdateShipCommand request, CancellationToken cancellationToken)
        {
            try
            {


                var ship = new ShipEntity(request.Name, request.Length, request.Width, request.Code);

                await _unitOfWork.ShipRepository.Update(request.Id, ship);

                var isUpdated = await _unitOfWork.SaveChangesAsync();

                if (isUpdated)
                    return ApiResponse.CreateSuccessResponse("Success", isUpdated);

                return ApiResponse.CreateFailedResponse("Unable to update ship record");


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
