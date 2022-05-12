using Newtonsoft.Json;
using OGFoodAPI.RecipeService.Models;
using OGFoodAPI.RecipeService.Strategies;

namespace OGFoodAPI.RecipeService
{
    public class InitializeLocalStorage
    {
        public async Task<string> GetRecipes(string search)
        {
            //Välj vilken strategy(api) som ska användas
            IRecipeContext recipeContext = new RecipeContext(new LocalStorage());

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
            ApiResponse response = await recipeContext.Request(recipeRequest);
            List<Recipe> recipes = new();

            if (response.succeeded)
            {
                
                recipes = recipeContext.DeserializeData(response.message, recipeRequest);
                string json = JsonConvert.SerializeObject(recipes);

                Console.WriteLine(json);

                return json;
            }

            return "Request didn't succeed";
        }

    }
}
