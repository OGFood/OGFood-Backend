using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller
{
    public interface IApiCaller
    {
        Recipe ProcessData(string data);
        ApiResponse Request(ApiRequest recipeRequest);
    }
}
