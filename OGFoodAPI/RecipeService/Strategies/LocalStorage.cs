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
    }
}
