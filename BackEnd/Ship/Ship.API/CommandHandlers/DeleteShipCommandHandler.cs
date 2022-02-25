using MediatR;
using Ship.API.ApiModels.Response;
using Ship.API.Commands;
using Ship.Core.IRepositories;

namespace Ship.API.CommandHandlers
{
    public class DeleteShipCommandHandler : IRequestHandler<DeleteShipCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteShipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                await _unitOfWork.ShipRepository.Delete(request.Id);

                var isDeleted = await _unitOfWork.SaveChangesAsync();

                if (isDeleted)
                    return ApiResponse.CreateSuccessResponse("Success", isDeleted);

                return ApiResponse.CreateFailedResponse("Unable to delete ship record");

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
