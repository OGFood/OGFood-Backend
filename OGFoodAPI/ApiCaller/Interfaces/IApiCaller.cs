using OGFoodAPI.ApiCaller.Models;

namespace OGFoodAPI.ApiCaller
{
    public interface IApiCaller
    {
        Recipe ProcessData(string data);
        Task<ApiResponse> Request(ApiRequest recipeRequest);
    }
}
