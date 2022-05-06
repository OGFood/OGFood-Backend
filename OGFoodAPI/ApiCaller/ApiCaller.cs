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

        public string BuildUrl(RecipeRequest recipeRequest)
        {
            return _apiCaller.BuildUrl(recipeRequest);
        }

        public Recipe ProcessData(string data)
        {
            return _apiCaller.ProcessData(data);
        }

        public RequestMessage Request()
        {
            return _apiCaller.Request();
        }
    }
}
