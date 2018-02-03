using System;
using Microsoft.Net.Http.Headers;

namespace U2U.AspNetCore.ScreenPlay
{
  public static class HttpClientExtensions
  {
    public static IHttpClient WithCookies(this IHttpClient httpClient)
    => httpClient.AddExtension(new CookiesHttpClientExtension());

    public static IHttpClient WithHeader(this IHttpClient httpClient, string name, string value)
    => httpClient.AddExtension(new HeaderHttpClientExtension(name, value));

    public static IHttpClient WithAcceptHeader(this IHttpClient httpClient, string mediaType)
    => httpClient.WithHeader(HeaderNames.Accept, mediaType);

    public static IHttpClient WithAcceptJsonHeader(this IHttpClient httpClient)
    => httpClient.WithAcceptHeader("application/json");
  }
}
