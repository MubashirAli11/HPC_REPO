using MediatR;
using Microsoft.AspNetCore.Identity;
using UserManagement.API.ApiModels.Response;
using UserManagement.API.Commands;
using UserManagement.Core.Entities;
using UserManagement.Core.IRepositories;
using UserManagement.Infrastructure.Services;

namespace UserManagement.API.CommandHandlers
{

    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly SignInManager<UserEntity> _signInManager;

        private readonly AuthenticationServices _authenticationServices;
        public LoginCommandHandler(
            IUnitOfWork unitOfWork,
            SignInManager<UserEntity> signInManager,
            AuthenticationServices authenticationServices)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _authenticationServices = authenticationServices;
        }

        public async Task<ApiResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);

                if (user == null)
                    return ApiResponse.CreateFailedResponse("user doesn't exists");

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

                if (result.Succeeded)
                {
                    return ApiResponse.CreateSuccessResponse("Success", _authenticationServices.GenerateAuthenticationToken());
                }
                else
                {
                    return ApiResponse.CreateFailedResponse("Invalid credentials");
                }

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
