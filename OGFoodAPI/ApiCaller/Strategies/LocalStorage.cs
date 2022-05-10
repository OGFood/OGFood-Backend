using OGFoodAPI.ApiCaller.Models;

namespace OGFoodAPI.ApiCaller.Strategies
{
    public class LocalStorage : IApiCaller
    {
        string Url { get; } = "localstorage.txt";

        public async Task<ApiResponse> Request(ApiRequest apiRequest)
        {
            ApiResponse response = new ApiResponse();

            if (!File.Exists(Url))
            {
                response.message = await File.ReadAllTextAsync(Url);
            }
            else
            {
                throw new Exception("Couldn't open " + Url + " in LocalStorage");
            }

            return response;
        }

        public Recipe ProcessData(string data)
        {
            return new Recipe() { str = data};
        }
    }
}
