using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller.Strats
{
    public class ApiCallStratergy : IApiCaller
    {
        public string BuildUrl(RecipeRequest recipeRequest)
        {
            return "";
        }

        public Recipe ProcessData(string data)
        {
            return new Recipe();
        }

        public RequestMessage Request()
        {
            return new RequestMessage();
        }
    }
}
