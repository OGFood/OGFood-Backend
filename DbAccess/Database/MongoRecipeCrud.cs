namespace DbAccess.Database
{
    using DbAccess.Interfaces;
    using DbAccess.Models;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MongoRecipeCrud : IRecipeCrud
    {
        private readonly IMongoCollection<Recipe> _recipes;

        public MongoRecipeCrud(MongoDbAccess dbAccess) => _recipes = dbAccess.RecipeCollection;
        public async Task<Recipe> GetRecipeById(string id)
        {
            var output = await _recipes.FindAsync(i => i.Id == id);
            return output.FirstOrDefault();
        }
        public async Task<Recipe> GetRecipeByName(string name)
        {
            var output = await _recipes.FindAsync(i => i.Name == name);
            return output.FirstOrDefault();
        }

#pragma warning disable RCS1155 // Use StringComparison when comparing strings.
        public async Task<List<Recipe>> GetRecipeByNameBeginsWith(string searchString)
        {
            var result = await _recipes.FindAsync(x =>
                x.Name.ToLower().StartsWith(searchString.ToLower()));
            return result.ToList();
        }
#pragma warning restore RCS1155 // Use StringComparison when comparing strings.
        public async Task<List<Recipe>> GetAllRecipes()
        {
            var result = await _recipes.FindAsync(_ => true);
            return result.ToList();
        }

        public async Task AddRecipeAsync(Recipe recipe) => await _recipes.InsertOneAsync(recipe);
        public async Task UpdateRecipeAsync(string id, Recipe recipe) => await _recipes.ReplaceOneAsync(x => x.Id == id, recipe);
        public async Task DeleteRecipeAsync(string id) => await _recipes.DeleteOneAsync(x => x.Id == id);
    }
}
