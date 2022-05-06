using OGFoodAPI.ApiCaller.Strats;
using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller
{
    public class SelectStrat
    {
        public void Go()
        {
            IApiCaller apiCaller = new ApiCaller(new PokeApiStrat());
            ApiResponse requestMessage;
            ApiRequest recipeRequest = new ApiRequest();

            requestMessage = apiCaller.Request(recipeRequest);

            if(requestMessage.succeeded)
                apiCaller.ProcessData(requestMessage.message);
        }
    }
}
