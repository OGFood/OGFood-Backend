using DbAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OGFoodAPI.RecipeService;
using OGFoodAPI.RecipeService.Models;
using OGFoodAPI.RecipeService.Strategies;

namespace OGFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRequestTest : ControllerBase
    {
        [HttpGet("{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Index(string search)
        {
            if (search == null)
                return "No data to show";

            var ingredient1 = new IngredientWithAmount() { Amount = 1, Ingredient = new Ingredient() { Name = "Lök", Id="1" } };
            var ingredient2 = new IngredientWithAmount() { Amount = 1, Ingredient = new Ingredient() { Name = "Tomatpure", Id="3" } };
            var ingredientList = new List<IngredientWithAmount>();
            ingredientList.Add(ingredient1);
            ingredientList.Add(ingredient2);

            var reqJson = JsonConvert.SerializeObject(ingredientList);

            Console.WriteLine("example request: " + reqJson);

            var searchLocalStorage = new InitializeLocalStorage();
            return await searchLocalStorage.GetRecipes(search);

            throw new Exception("Error requesting data");
        }
    }
}
