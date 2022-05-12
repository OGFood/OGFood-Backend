using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService
{
    public class RecipeContext : IRecipeContext
    {
        readonly IRecipeContext _apiCaller;
        public RecipeContext(IRecipeContext apiCaller)
        {
            _apiCaller = apiCaller;
        }

        public List<Recipe> DeserializeAndProcessData(string data, ApiRequest apiRequest)
        {
            return _apiCaller.DeserializeAndProcessData(data, apiRequest);
        }

        public Task<string> GetRecipes(string search)
        {
            return _apiCaller.GetRecipes(search);
        }

        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            return await _apiCaller.Request(apiRequest);
        }
    }
}
