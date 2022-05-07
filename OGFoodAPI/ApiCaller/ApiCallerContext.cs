using OGFoodAPI.ApiCaller.Models;

namespace OGFoodAPI.ApiCaller
{
    public class ApiCallerContext : IApiCaller
    {
        readonly IApiCaller _apiCaller;
        public ApiCallerContext(IApiCaller apiCaller)
        {
            _apiCaller = apiCaller;
        }

        public Recipe ProcessData(string data)
        {
            return _apiCaller.ProcessData(data);
        }

        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            return await _apiCaller.Request(apiRequest);
        }
    }
}
