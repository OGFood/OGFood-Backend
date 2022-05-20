using OGFoodAPI.DbService;
using OGFoodAPI.DbService.CrudHelpers;
using SharedInterfaces.Models;

namespace OGFoodAPI.RecipeService.Strategies
{
    public class DbStorage : IRecipeContext
    {
        readonly MongoDbContext _dbContext;

        protected List<Recipe> _recipes = new();
        protected List<Ingredient> _ingredients = new();

        public DbStorage(MongoDbContext dbContext)
        {
            _dbContext = dbContext;

            RefreshCache();
        }
        protected virtual async void RefreshCache()
        {
            var RecipeCrud = new MongoRecipeCrud(_dbContext);
            _recipes = await RecipeCrud.GetAllRecipes();

            var IngredientCrud = new MongoIngredientCrud(_dbContext);
            _ingredients = await IngredientCrud.GetAllIngredients();
        }

        public async Task<Recipe> Delete(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Recipe>> Get(Recipe recipe)
        {
            var res = _recipes.Where(q => q.Id == recipe.Id);

            return res.ToList();
        }

        public async Task<List<Recipe>> Patch(Recipe recipe, Recipe recipeUpdated)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Recipe>> Post(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> Put(Recipe recipe, Recipe recipeUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
