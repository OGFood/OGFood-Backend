using OGFoodAPI.RecipeService.Models;

namespace OGFoodAPI.RecipeService.Strategies
{
    public class PokeApiStrat : IRecipeContext
    {
        string Url { get; } = "https://pokeapi.co/api/v2/";

        public static async Task<string> ApiRequest(string url)
        {
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;

            return await content.ReadAsStringAsync();
        }
        
        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            string req = Url + "pokemon?offset=20&limit=20";

            return new ApiResponse() { succeeded = true, message = await ApiRequest(req) };
        }

        public List<Recipe> DeserializeData(string data, ApiRequest apiRequest)
        {
            var recipes = new List<Recipe>();
            recipes.Add(new Recipe() { Description = data });

            return recipes;
        }
    }
}
