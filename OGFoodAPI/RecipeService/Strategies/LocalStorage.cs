using Newtonsoft.Json;
using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService.Strategies
{
    public class LocalStorage : IRecipeService
    {
        string Url { get; } = "recipes.json";

        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            ApiResponse response = new ApiResponse();

            if (await Task.Run(() => File.Exists(Url)))
            {
                Console.WriteLine("File exists!!!");
                response.message = await File.ReadAllTextAsync(Url);
                response.succeeded = true;
            }
            else
            {
                throw new Exception("Couldn't open " + Url + " in LocalStorage");
            }

            return response;
        }

        public List<Recipe> DeserializeData(string data)
        {
            /*
            Recepten är redan sparade som ett List<Recipe>-object och behöver ingen ytterliggare bearbetning.
            */
            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(data);

            return recipes;
        }
    }
}
