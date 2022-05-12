using Newtonsoft.Json;
using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService.Strategies
{
    public class LocalStorage : IRecipeContext
    {
        string Url { get; } = "recipes.json";

        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            ApiResponse response = new ApiResponse();

            if (await Task.Run(() => File.Exists(Url)))
            {
                response.message = await File.ReadAllTextAsync(Url);
                response.succeeded = true;
            }
            else
            {
                throw new Exception("Couldn't open " + Url + " in LocalStorage");
            }

            return response;
        }

        public List<Recipe> DeserializeData(string data, ApiRequest apiRequest)
        {
            /*
            Recepten är redan sparade som ett List<Recipe>-object och behöver ingen ytterliggare bearbetning.
            */
            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(data);

            List<Recipe> results = new List<Recipe>();
            recipes.ForEach(recipe =>
            {
                Console.WriteLine("Name of recipe " + recipe.Name);
                Console.WriteLine("Length of ingredientWithAmount: " + recipe.IngredientsWithAmount.Count);
                int ingredientCount = 0;
                recipe.IngredientsWithAmount.ForEach(recipeIngredient =>
                {
                    Console.WriteLine("Name of ingredient: " + recipeIngredient.Ingredient.Name);
                    Console.WriteLine("storage: " + JsonConvert.SerializeObject(recipeIngredient));
                    apiRequest.IngredientsWithAmount.ForEach(req =>
                    {
                        if (req.Ingredient.Id == recipeIngredient.Ingredient.Id && req.Amount >= recipeIngredient.Amount)
                        {//Måste kolla alla ingredienser
                            ingredientCount++;
                            Console.WriteLine("req amount: " + req.Amount);
                            Console.WriteLine("recipeIngredient amount: " + recipeIngredient.Amount);
                        }
                    });
                });

                if (!results.Contains(recipe) && ingredientCount == recipe.IngredientsWithAmount.Count)
                {
                    results.Add(recipe);
                }
            });

            return results;

        }

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
