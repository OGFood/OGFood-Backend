using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller
{
    public class ApiCaller : IApiCaller
    {
        readonly IApiCaller _apiCaller;
        public ApiCaller(IApiCaller apiCaller)
        {
            _apiCaller = apiCaller;
        }

        public Recipe ProcessData(string data)
        {
            return _apiCaller.ProcessData(data);
        }

        public ApiResponse Request(ApiRequest apiRequest)
        {
            return _apiCaller.Request(apiRequest);
        }
    }
}
