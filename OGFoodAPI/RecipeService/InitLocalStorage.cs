using DbAccess.Models;
using Newtonsoft.Json;
using OGFoodAPI.RecipeService.Models;
using OGFoodAPI.RecipeService.Strategies;

namespace OGFoodAPI.RecipeService
{
    public class InitLocalStorage
    {
        public async Task<string> Go()
        {
            //Välj vilken strategy(api) som ska användas
            IRecipeService apiCaller = new RecipeContext(new LocalStorage());

            /*
            var intable = new List<IngredientWithAmount>();
            intable.Add(new IngredientWithAmount() { Ingredient = new Ingredient() { Name = "lÖÖK" } });

            List<Recipe> makeJsonRecipes = new();
            makeJsonRecipes.Add(new Recipe() { IngredientsTable = intable });
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\recipebase.json", JsonConvert.SerializeObject(makeJsonRecipes));
            */

            //Sökningen till API:n ska ligga i ett ApiRequest-objekt
            ApiRequest recipeRequest = new();

            //Lägger till ingredient och mängd med hjälp av IngredientWithAmount
            IngredientWithAmount ingredientWithAmount = new IngredientWithAmount();
            ingredientWithAmount.Ingredient.Name = "Lök";
            ingredientWithAmount.Amount = 1;

            recipeRequest.IngredientsWithAmount.Add(ingredientWithAmount);

            //Svar från API
            ApiResponse response = await apiCaller.Request(recipeRequest);
            List<Recipe> recipes = new();

            if(response.succeeded)
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
