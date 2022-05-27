namespace DbAccess.Database
{
    using DbAccess.Interfaces;
    using MongoDB.Driver;
    using SharedInterfaces.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MongoIngredientCrud : IIngredientCrud
    {
        private readonly IMongoCollection<Ingredient> ingredients;

        public MongoIngredientCrud(MongoDbAccess dbAccess) => ingredients = dbAccess.IngredientCollection;
        public async Task<Ingredient> GetIngredientById(string id)
        {
            var output = await ingredients.FindAsync(i => i.Id == id);
            return output.FirstOrDefault();
        }
        public async Task<Ingredient> GetIngredientByName(string name)
        {
            var output = await ingredients.FindAsync(i => i.Name == name);
            return output.FirstOrDefault();
        }

#pragma warning disable RCS1155 // Use StringComparison when comparing strings.
        public async Task<List<Ingredient>> GetIngredientsByNameBeginsWith(string searchString)
        {
            var result = await ingredients.FindAsync(x =>
                x.Name.ToLower().StartsWith(searchString.ToLower()));
            return result.ToList();
        }
#pragma warning restore RCS1155 // Use StringComparison when comparing strings.
        public async Task<List<Ingredient>> GetAllIngredients()
        {
            var result = await ingredients.FindAsync(_ => true);
            return result.ToList().OrderBy(x => x.Name).ToList();
        }

        public async Task AddIngredientAsync(Ingredient ingredient) => await ingredients.InsertOneAsync(ingredient);
        public async Task UpdateIngredientAsync(string id, Ingredient ingredient) => await ingredients.ReplaceOneAsync(x => x.Id==id, ingredient);
        public async Task DeleteIngredientAsync(string id) => await ingredients.DeleteOneAsync(x => x.Id == id);
    }
}
