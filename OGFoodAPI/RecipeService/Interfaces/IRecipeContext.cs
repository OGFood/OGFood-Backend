using SharedInterfaces.Models;

namespace OGFoodAPI.RecipeService
{
    public interface IRecipeContext
    {
        public Task<List<Recipe>> Get(Recipe recipe);
        public Task<List<Recipe>> Post(Recipe recipe);
        public Task<Recipe> Put(Recipe recipe, Recipe recipeUpdated);
        public Task<List<Recipe>> Patch(Recipe recipe, Recipe recipeUpdated);
        public Task<Recipe> Delete(Recipe recipe);
    }
}
