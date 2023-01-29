namespace Jobs.Mvc.Http;

public interface IHttpClient
{
    Task<string> GetStringAsync(string url);
    Task<HttpResponseMessage> PostAsync<T>(string uri, T item);
}
