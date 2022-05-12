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

        public List<Recipe> DeserializeData(string data, ApiRequest apiRequest)
        {
            return _apiCaller.DeserializeData(data, apiRequest);
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
