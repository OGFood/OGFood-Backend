using SharedInterfaces.Models;

namespace OGFoodAPI.RecipeService
{
    public class RecipeContext : IRecipeContext
    {
        readonly IRecipeContext _context;
        public RecipeContext(IRecipeContext context) => _context = context;

        public async Task<Recipe> Delete(Recipe recipe) => await _context.Delete(recipe);

        public async Task<List<Recipe>> Get(Recipe recipe) => await _context.Get(recipe);

        public async Task<List<Recipe>> Patch(Recipe recipe, Recipe recipeUpdated) => await _context.Patch(recipe, recipeUpdated);

        public async Task<List<Recipe>> Post(Recipe recipe) => await _context.Post(recipe);

        public async Task<Recipe> Put(Recipe recipe, Recipe updatedRecipe) => await _context.Put(recipe, updatedRecipe);
    }
}
