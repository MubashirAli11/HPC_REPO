using MediatR;
using UserManagement.API.ApiModels.Response;

namespace UserManagement.API.Commands
{
    public class LoginCommand : IRequest<ApiResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
