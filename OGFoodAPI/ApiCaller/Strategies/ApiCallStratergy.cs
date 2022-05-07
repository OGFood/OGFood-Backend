using OGFoodAPI.ApiCaller.Models;

namespace OGFoodAPI.ApiCaller.Strategies
{
    public class ApiCallStratergy : IApiCaller
    {
        public Recipe ProcessData(string data)
        {
            return new Recipe();
        }

        public Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            return null;
        }
    }
}
