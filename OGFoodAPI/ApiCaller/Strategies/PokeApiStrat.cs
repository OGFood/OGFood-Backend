using OGFoodAPI.ApiCaller.Models;

namespace OGFoodAPI.ApiCaller.Strategies
{
    public class PokeApiStrat : IApiCaller
    {
        string url { get; } = "https://pokeapi.co/api/v2/";

        public static async Task<string> ApiRequest(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;

            return await content.ReadAsStringAsync();
        }

        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            string req = url + "pokemon?offset=20&limit=20";

            return new ApiResponse() { succeeded = true, message = await ApiRequest(req) };
        }

        public Recipe ProcessData(string data)
        {
            return new Recipe() { str = data};
        }
    }
}
