using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller.Strats
{
    public class ApiCallStratergy : IApiCaller
    {
        public Recipe ProcessData(string data)
        {

            return new Recipe();
        }

        public ApiResponse Request(ApiRequest apiRequest)
        {
            return new ApiResponse();
        }
    }
}
