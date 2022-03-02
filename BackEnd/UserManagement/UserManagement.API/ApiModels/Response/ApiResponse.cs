using Newtonsoft.Json;

namespace UserManagement.API.ApiModels.Response
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object? Data { get; set; }

        public ApiResponse(bool isSuccess, int statusCode, string message, object? data)
        {
            this.IsSuccess = isSuccess;
            this.StatusCode = statusCode;
            this.Message = message;
            this.Data = data;

        }

        public static ApiResponse CreateSuccessResponse(string message, object data)
        {
            return new ApiResponse(true, 200, message, data);
        }


        public static ApiResponse CreateFailedResponse(string message)
        {
            return new ApiResponse(false, 400, message, null);
        }

    }
}
