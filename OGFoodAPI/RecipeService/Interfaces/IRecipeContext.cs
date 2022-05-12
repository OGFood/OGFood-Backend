using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService
{
    public interface IRecipeContext
    {
        List<Recipe> DeserializeAndProcessData(string data, ApiRequest apiRequest);
        Task<ApiResponse> Request(ApiRequest recipeRequest);
        Task<string> GetRecipes(string search);
    }
}
