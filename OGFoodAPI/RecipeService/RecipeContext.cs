using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService
{
    public class RecipeContext : IRecipeService
    {
        readonly IRecipeService _apiCaller;
        public RecipeContext(IRecipeService apiCaller)
        {
            _apiCaller = apiCaller;
        }

        public List<Recipe> DeserializeData(string data)
        {
            return _apiCaller.DeserializeData(data);
        }

        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            return await _apiCaller.Request(apiRequest);
        }
    }
}
