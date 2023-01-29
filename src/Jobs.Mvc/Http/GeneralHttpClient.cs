using System.Text.Json;

namespace Jobs.Mvc.Http;

public class GeneralHttpClient : IHttpClient
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GetStringAsync(string uri)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await client.SendAsync(requestMessage);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(item), System.Text.Encoding.UTF8, "application/json")
        };
        var response = await client.SendAsync(requestMessage);
        if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        {
            throw new HttpRequestException();
        }
        return response;
    }
}
