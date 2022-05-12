using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService
{
    public interface IRecipeContext
    {
        List<Recipe> DeserializeData(string data, ApiRequest apiRequest);
        Task<ApiResponse> Request(ApiRequest recipeRequest);
    }
}
