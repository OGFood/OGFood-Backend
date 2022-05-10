using OGFoodAPI.ApiCaller.Models;
using OGFoodAPI.ApiCaller.Strategies;

namespace OGFoodAPI.ApiCaller
{
    public class SelectStrat
    {
        public async Task<Recipe> Go()
        {
            //Välj vilken strategy(api) som ska användas
            IApiCaller apiCaller = new ApiCallerContext(new LocalStorage());

            //Data som API:n behöver ska ligga i ett ApiRequest-objekt
            ApiRequest recipeRequest = new();

            //Svar från API
            ApiResponse requestMessage = await apiCaller.Request(recipeRequest);

            if(requestMessage.succeeded)
            {
                //Processering/deserialisering av data sker i ProcessData
                return apiCaller.ProcessData(requestMessage.message);
            }

            return new Recipe() { str = "Data error"};
        }
    }
}
