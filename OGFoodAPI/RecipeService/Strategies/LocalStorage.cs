using Newtonsoft.Json;
using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService.Strategies
{
    public class LocalStorage : IRecipeContext
    {
        string Url { get; } = "recipes.json";

        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            ApiResponse response = new();

            if (await Task.Run(() => File.Exists(Url)))
            {
                response.Message = await File.ReadAllTextAsync(Url);
                response.Succeeded = true;
            }
            else
            {
                response.Message = "Couldn't open " + Url + " in LocalStorage";
            }

            return response;
        }

        public List<Recipe> DeserializeAndProcessData(string data, ApiRequest apiRequest)
        {
            List<Recipe>? recipes = JsonConvert.DeserializeObject<List<Recipe>>(data) ?? new();
            List<Recipe> results = new();

            recipes.ForEach(recipe =>
            {
                int ingredientCount = 0;
                recipe.IngredientsWithAmount.ForEach(recipeIngredient =>
                {
                    apiRequest.IngredientsWithAmount.ForEach(req =>
                    {
                        if (req.Ingredient.Id == recipeIngredient.Ingredient.Id && req.Amount >= recipeIngredient.Amount)
                        {
                            ingredientCount++;
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
            //Ens ingredienser ska ligga i ett ApiRequest-objekt
            ApiRequest recipeRequest = new();
            List<IngredientWithAmount>? ingredientsWithAmount;

            try
            {
                ingredientsWithAmount = JsonConvert.DeserializeObject<List<IngredientWithAmount>>(search) ?? new();
            }
            catch (Exception ex)
            {
                return "Error deserializing search. Error:" + ex;
            }

            recipeRequest.IngredientsWithAmount = ingredientsWithAmount;

            //Svar från API
            ApiResponse response = await Request(recipeRequest);

            if (response.Succeeded)
                return JsonConvert.SerializeObject(DeserializeAndProcessData(response.Message, recipeRequest));

            return "Request didn't succeed";
        }
    }
}
