using Microsoft.AspNetCore.Mvc;
using OGFoodAPI.ApiCaller;
using OGFoodAPI.ApiCaller.Models;

namespace OGFoodAPI.Controllers
{
    public class ApiRequestTest : ControllerBase
    {
        [Route("test")]
        [HttpGet]
        public async Task<string> Index()
        {
            SelectStrat callApi = new();
            Recipe recipe = await callApi.Go();

            return recipe.str;
        }
    }
}
