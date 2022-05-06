using OGFoodAPI.ApiCaller.Strats;

namespace OGFoodAPI.ApiCaller
{
    public class SelectStrat
    {
        IApiCaller apiCaller = new ApiCaller(new ApiCallStratergy());
    }
}
