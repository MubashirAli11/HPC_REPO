using Ship.API.ApiModels.Response;
using System.Net;
using System.Text.Json;

namespace Ship.API.ExceptionHandler
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
