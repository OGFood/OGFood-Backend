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
        readonly IRecipeContext _recipeContext;

        public ApiRequestTest(IRecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }

        [HttpGet("{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Index(string search)
        {
            if (search == null)
                return "No data to show";

            //Det här är endast för att ge  exempel på en söksträng man kan använda
            var ingredient1 = new IngredientWithAmount() { Amount = 1, Ingredient = new Ingredient() { Id = "1", Name = "Lök" } };
            var ingredient2 = new IngredientWithAmount() { Amount = 1, Ingredient = new Ingredient() { Id = "2", Name = "Falukorv" } };
            var ingredient3 = new IngredientWithAmount() { Amount = 2, Ingredient = new Ingredient() { Id = "3", Name = "Tomatpuré" } };
            var ingredientList = new List<IngredientWithAmount>
            {
                ingredient1,
                ingredient2,
                ingredient3
            };

            var reqJson = JsonConvert.SerializeObject(ingredientList);

            Console.WriteLine("example request: " + reqJson);
            //----------------------------------------------------------------------

            return await _recipeContext.GetRecipes(search);

            throw new Exception("Error requesting data");
        }
    }
}
