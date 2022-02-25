using Newtonsoft.Json;

namespace Ship.API.ApiModels.Response
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        [JsonProperty("Data", NullValueHandling = NullValueHandling.Ignore)]
        public object? Data { get; set; }
        [JsonProperty("Total", NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }
        [JsonProperty("PageIndex", NullValueHandling = NullValueHandling.Ignore)]
        public int? PageIndex { get; set; }
        [JsonProperty("PageCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? PageSize { get; set; }

        public ApiResponse(bool isSuccess, int statusCode, string message, object? data, int? total, int? pageIndex, int? pageSize)
        {
            this.IsSuccess = isSuccess; 
            this.StatusCode = statusCode;
            this.Message= message;
            this.Data = data;
            this.Total = total;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public static ApiResponse CreateSuccessResponse(string message, object data)
        {
            return new ApiResponse(true, 200, message, data, null, null, null);
        }


        public static ApiResponse CreateFailedResponse(string message)
        {
            return new ApiResponse(false, 400, message, null, null, null, null);
        }

        public static ApiResponse CreatePaginationResponse(string message, object data, int? total, int? pageIndex, int? pageSize)

        {
            return new ApiResponse(true, 200, message, data, total, pageIndex, pageSize);
        }
    }
}
