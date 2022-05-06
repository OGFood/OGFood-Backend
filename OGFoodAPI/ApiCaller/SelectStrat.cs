using OGFoodAPI.ApiCaller.Strats;
using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.ApiCaller
{
    public class SelectStrat
    {
        public void Go()
        {
            IApiCaller apiCaller = new ApiCaller(new ApiCallStratergy());
            RequestMessage requestMessage;
            RecipeRequest recipeRequest = new RecipeRequest();

            apiCaller.BuildUrl(recipeRequest);
            requestMessage = apiCaller.Request();

            if(requestMessage.succeeded)
                apiCaller.ProcessData(requestMessage.message);
        }
    }
}
