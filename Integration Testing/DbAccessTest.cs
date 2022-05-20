using NUnit.Framework;
using OGFoodAPI.DbService;
using OGFoodAPI.DbService.CrudHelpers;
using System.Threading.Tasks;
using static DbAccess.Factory;

namespace Integration_Testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ConnectRecipeCollection()
        {
            //var csh = new ConnectionStringHelper();
            //var dbService = new MongoDbContext(csh.ConnectionString);
            //var crud = new MongoIngredientCrud(dbService);
            var sut = GetRecipeCrud();

            var result = await sut.GetAllRecipes();

            Assert.AreNotEqual(0, result.Count);
        }

        [Test]
        public async Task ConnectIngredientCollection()
        {
            //var csh = new ConnectionStringHelper();
            //var dbService = new MongoDbContext(csh.ConnectionString);
            //var crud = new MongoIngredientCrud(dbService);
            var sut = GetIngredientCrud();

            var result = await sut.GetAllIngredients();

            Assert.AreNotEqual(0, result.Count);
        }
    }
}