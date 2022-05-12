using DbAccess.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using OGFoodAPI.RecipeService;
using OGFoodAPI.RecipeService.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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
        public async Task LocalStorage_Test_Korvstroganoff(int a, int b, int c)
        {
            var ingredient1 = new IngredientWithAmount() { Amount = a, Ingredient = new Ingredient() { Id="1", Name = "Lök" } };
            var ingredient2 = new IngredientWithAmount() { Amount = b, Ingredient = new Ingredient() { Id="2", Name = "Falukorv" } };
            var ingredient3 = new IngredientWithAmount() { Amount = c, Ingredient = new Ingredient() { Id="3", Name = "Tomatpuré" } };
            var ingredientList = new List<IngredientWithAmount>();
            ingredientList.Add(ingredient1);
            ingredientList.Add(ingredient2);
            ingredientList.Add(ingredient3);

            var reqJson = JsonConvert.SerializeObject(ingredientList);
            Debug.WriteLine(reqJson);
            var searchLocalStorage = new InitializeLocalStorage();
            var actual = await searchLocalStorage.GetRecipes(reqJson);
            var expected = "[{\"Name\":\"Korvstroganoff\",\"Description\":\"Klassisk jävla korvstroganoff\",\"IngredientsWithAmount\":[{\"Ingredient\":{\"Id\":\"1\",\"Name\":\"Lök\"},\"Amount\":1.0,\"Unit\":\"\"},{\"Ingredient\":{\"Id\":\"2\",\"Name\":\"Falukorv\"},\"Amount\":0.5,\"Unit\":\"\"},{\"Ingredient\":{\"Id\":\"3\",\"Name\":\"Tomatpuré\"},\"Amount\":2.0,\"Unit\":\"\"}]}]";

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 1, 1)]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 0, 1)]
        public async Task LocalStorage_Test_NoRecipesFound(int a, int b, int c)
        {
            var ingredient1 = new IngredientWithAmount() { Amount = a, Ingredient = new Ingredient() { Id = "1", Name = "Lök" } };
            var ingredient2 = new IngredientWithAmount() { Amount = b, Ingredient = new Ingredient() { Id = "2", Name = "Falukorv" } };
            var ingredient3 = new IngredientWithAmount() { Amount = c, Ingredient = new Ingredient() { Id = "3", Name = "Tomatpuré" } };
            var ingredientList = new List<IngredientWithAmount>();
            ingredientList.Add(ingredient1);
            ingredientList.Add(ingredient2);
            ingredientList.Add(ingredient3);

            var reqJson = JsonConvert.SerializeObject(ingredientList);
            Debug.WriteLine(reqJson);
            var searchLocalStorage = new InitializeLocalStorage();
            var actual = await searchLocalStorage.GetRecipes(reqJson);
            var expected = "[]";

            Assert.AreEqual(expected, actual);
        }
    }
}