using System.Text.Json;
using UserManagement.API.ApiModels.Response;

namespace UserManagement.API.ExceptionHandler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var failedResponse = ApiResponse.CreateFailedResponse(error.Message);

                var result = JsonSerializer.Serialize(failedResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
