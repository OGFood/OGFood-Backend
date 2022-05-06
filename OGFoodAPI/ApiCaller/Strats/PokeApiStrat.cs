using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller.Strats
{
    public class PokeApiStrat : IApiCaller
    {
        public string BuildUrl(RecipeRequest recipeRequest)
        {
            return "https://pokeapi.co/api/v2/pokemon?offset=20&limit=20";
        }

        public Recipe ProcessData(string data)
        {

            return new Recipe();
        }

        public RequestMessage Request()
        {
            return new RequestMessage();
        }
    }
}
