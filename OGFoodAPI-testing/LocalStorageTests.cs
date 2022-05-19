using Newtonsoft.Json;
using NUnit.Framework;
using SharedInterfaces.Models;
using OGFoodAPI.RecipeService;
using OGFoodAPI.RecipeService.Strategies;
using System.Collections.Generic;
using System.Diagnostics;

namespace OGFoodAPI_testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(5,5,5)]
        [TestCase(1, 1, 2)]
        public void LocalStorage_Test_Korvstroganoff(int a, int b, int c)
        {
            var ingredient1 = new SharedInterfaces.Models.IngredientWithAmount() { Amount = a, Ingredient = new Ingredient() { Id="1", Name = "Lok" } };
            var ingredient2 = new IngredientWithAmount() { Amount = b, Ingredient = new Ingredient() { Id="2", Name = "Falukorv" } };
            var ingredient3 = new IngredientWithAmount() { Amount = c, Ingredient = new Ingredient() { Id="3", Name = "Tomatpuré" } };
            var ingredientList = new List<IngredientWithAmount>
            {
                ingredient1,
                ingredient2,
                ingredient3
            };

            var reqJson = JsonConvert.SerializeObject(ingredientList);
            Debug.WriteLine(reqJson);
            var searchLocalStorage = new RecipeContext(new LocalStorage());
            var actual = searchLocalStorage.Get(new Recipe());
            const string expected = "[{\"Name\":\"Korvstroganoff\",\"Description\":\"Klassisk javla korvstroganoff\",\"IngredientsWithAmount\":[{\"Ingredient\":{\"Id\":\"1\",\"Name\":\"Lok\"},\"Amount\":1.0,\"Unit\":\"\"},{\"Ingredient\":{\"Id\":\"2\",\"Name\":\"Falukorv\"},\"Amount\":0.5,\"Unit\":\"\"},{\"Ingredient\":{\"Id\":\"3\",\"Name\":\"Tomatpure\"},\"Amount\":2.0,\"Unit\":\"\"}]}]";

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 1, 1)]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 0, 1)]
        public void LocalStorage_Test_NoRecipesFound(int a, int b, int c)
        {
            var ingredient1 = new IngredientWithAmount() { Amount = a, Ingredient = new Ingredient() { Id = "1", Name = "Lok" } };
            var ingredient2 = new IngredientWithAmount() { Amount = b, Ingredient = new Ingredient() { Id = "2", Name = "Falukorv" } };
            var ingredient3 = new IngredientWithAmount() { Amount = c, Ingredient = new Ingredient() { Id = "3", Name = "Tomatpuré" } };
            var ingredientList = new List<IngredientWithAmount>
            {
                ingredient1,
                ingredient2,
                ingredient3
            };

            var reqJson = JsonConvert.SerializeObject(ingredientList);
            Debug.WriteLine(reqJson);
            var searchLocalStorage = new RecipeContext(new LocalStorage());
            var actual = searchLocalStorage.Get(new Recipe());
            const string expected = "[]";

            Assert.AreEqual(expected, actual);
        }
    }
}