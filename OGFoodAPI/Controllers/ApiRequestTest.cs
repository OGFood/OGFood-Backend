using Microsoft.AspNetCore.Mvc;
using OGFoodAPI.ApiCaller;

namespace OGFoodAPI.Controllers
{
    public class ApiRequestTest : ControllerBase
    {
        [Route("test")]
        public string Index()
        {
            SelectStrat callApi = new SelectStrat();
            callApi.Go();

            return "test";
        }
    }
}
