using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller
{
    public interface IApiCaller
    {
        Recipe ProcessData(string data);
        string BuildUrl(RecipeRequest recipeRequest);
        RequestMessage Request();
    }
}
