using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace Jobs.Mvc.Infrastructure;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string>()
            {
                authorizationHeader
            });
        }

        var token = await GetToken();

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetToken()
    {
        const string ACCESS_TOKEN = "access_token";
        return await httpContextAccessor.HttpContext.GetTokenAsync(ACCESS_TOKEN);
    }
}