using OGFoodAPI.ApiCaller.Strats;
using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller
{
    public class SelectStrat
    {

        public async Task<Recipe> Go()
        {
            IApiCaller apiCaller = new ApiCaller(new PokeApiStrat());
            ApiResponse requestMessage;
            ApiRequest recipeRequest = new ApiRequest();

            requestMessage = await apiCaller.Request(recipeRequest);

            if(requestMessage.succeeded)
                return apiCaller.ProcessData(requestMessage.message);

            return new Recipe();
        }
    }
}
