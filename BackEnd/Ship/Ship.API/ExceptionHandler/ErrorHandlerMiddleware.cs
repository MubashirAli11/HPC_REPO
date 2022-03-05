using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

                ApiResponse apiRespponse;

                if (response.StatusCode == 403)
                    apiRespponse = ApiResponse.CreateForbiddenResponse(error.Message);
                else
                    apiRespponse = ApiResponse.CreateFailedResponse(error.Message);

                var result = JsonConvert.SerializeObject(apiRespponse, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                await response.WriteAsync(result);
            }
        }
    }
}
