using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService
{
    public interface IRecipeService
    {
        List<Recipe> DeserializeData(string data);
        Task<ApiResponse> Request(ApiRequest recipeRequest);
    }
}
