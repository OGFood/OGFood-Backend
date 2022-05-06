namespace DbAccess.Database
{
    using Interfaces;
    using MongoDB.Driver;
    using Models;

    internal class MongoDbAccess
    {
        private readonly string connectionString;

        private const string databaseName = "GoodFood";
        private const string ingredientCollection = "Ingredients";

        public IMongoCollection<Ingredient> IngredientCollection { get => MongoConnect<Ingredient>(ingredientCollection);}

        public MongoDbAccess(IConnectionStringHelper csh) => connectionString = csh.ConnectionString;

        private IMongoCollection<T> MongoConnect<T>(in string collection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            return db.GetCollection<T>(collection);
        }
    }
}
