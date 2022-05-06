namespace DbAccess.Database
{
    using DbAccess.Interfaces;
    using DbAccess.Models;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class MongoIngredientCrud : IIngredientCrud
    {
        private readonly IMongoCollection<Ingredient> ingredients;

        public MongoIngredientCrud(MongoDbAccess dbAccess)
        {
            ingredients = dbAccess.IngredientCollection;
        }
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
        public async Task<List<Ingredient>> GetIngredientsByNameBeginsWith(string searchString)
        {
            var result = await ingredients.FindAsync(x =>
                x.Name.ToLower().StartsWith(searchString.ToLower()));
            return result.ToList();
        }
    }
}
