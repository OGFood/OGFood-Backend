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

            //Välj vilken strategy(api) som ska användas
            IRecipeService apiCaller = new RecipeContext(new LocalStorage());

            //Sökningen till API:n ska ligga i ett ApiRequest-objekt
            ApiRequest recipeRequest = new();
            List<IngredientWithAmount> ingredientsWithAmount = new();

            try
            {
                ingredientsWithAmount = JsonConvert.DeserializeObject<List<IngredientWithAmount>>(search);
            }
            catch (Exception ex)
            {
                return "Error deserializing search";
            }


            recipeRequest.IngredientsWithAmount = ingredientsWithAmount;

            //Svar från API
            ApiResponse response = await apiCaller.Request(recipeRequest);
            List<Recipe> recipes = new();

            if (response.succeeded)
            {
                recipes = apiCaller.DeserializeData(response.message);

                string json = JsonConvert.SerializeObject(recipes);

                Console.WriteLine(json);

                return json;
            }

            throw new Exception("Error requesting data");
        }
    }
}
