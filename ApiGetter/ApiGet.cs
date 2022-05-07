using System.Net.Http;
using System.Net.Http.Headers;

namespace ApiGetter
{

    static class ApiGet
    {
        public static async Task<string> ApiRequest(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;

            return await content.ReadAsStringAsync();
        }
    }
}
