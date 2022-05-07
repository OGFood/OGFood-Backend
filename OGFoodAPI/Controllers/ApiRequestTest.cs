using Microsoft.AspNetCore.Mvc;
using OGFoodAPI.ApiCaller;
using OGFoodAPI.SharedApiCom;

namespace OGFoodAPI.Controllers
{
    public class ApiRequestTest : ControllerBase
    {
        [Route("test")]
        public async Task<string> Index()
        {
            SelectStrat callApi = new SelectStrat();
            Recipe recipe = await callApi.Go();

            return recipe.str;
        }
    }
}
