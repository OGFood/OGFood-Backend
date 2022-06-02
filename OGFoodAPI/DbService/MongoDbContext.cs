using MongoDB.Driver;
using SharedInterfaces.Models;

namespace OGFoodAPI.DbService
{
    public class MongoDbContext
    {
        private readonly string _connectionString;

        private const string databaseName = "GoodFood";

        private const string ingredientCollection = "Ingredients";
        private const string userCollection = "Users";
        private const string recipeCollection = "Recipes";

        public IMongoCollection<Ingredient> IngredientCollection { get => MongoConnect<Ingredient>(ingredientCollection); }
        public IMongoCollection<User> UserCollection { get => MongoConnect<User>(userCollection); }
        public IMongoCollection<Recipe> RecipeCollection { get => MongoConnect<Recipe>(recipeCollection); }

        public MongoDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IMongoCollection<T> MongoConnect<T>(in string collection)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase(databaseName);
            return db.GetCollection<T>(collection);
        }
    }
}