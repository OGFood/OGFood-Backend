using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller.Strats
{
    public class PokeApiStrat : IApiCaller
    {
        public string url { get; set; } = "https://pokeapi.co/api/v2/";

        public Recipe ProcessData(string data)
        {

            return new Recipe();
        }

        public ApiResponse Request(ApiRequest apiRequest)
        {
            string req = url + "pokemon?offset=20&limit=20";



            return new ApiResponse();
        }
    }
}
