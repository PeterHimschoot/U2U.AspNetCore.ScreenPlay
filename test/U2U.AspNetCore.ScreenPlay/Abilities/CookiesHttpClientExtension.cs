using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;

namespace U2U.AspNetCore.ScreenPlay
{
  public class CookiesHttpClientExtension : IHttpClientExtension
  {
    public virtual void BeforeRequest(RequestBuilder requestBuilder, Uri absoluteUrl)
    => AddCookies(requestBuilder, absoluteUrl);

    public virtual void AfterResponse(HttpResponseMessage response, Uri absoluteUrl)
    => UpdateCookies(response, absoluteUrl);

    protected CookieContainer Cookies { get; } = new CookieContainer();

    protected void UpdateCookies(HttpResponseMessage response, Uri absoluteUrl)
    {
      if (response.Headers.Contains(HeaderNames.SetCookie))
      {
        var cookies = response.Headers.GetValues(HeaderNames.SetCookie);
        foreach (var cookie in cookies)
        {
          Cookies.SetCookies(absoluteUrl, cookie);
        }
      }
    }

    protected void AddCookies(RequestBuilder requestBuilder, Uri absoluteUrl)
    {
      var cookieHeader = Cookies.GetCookieHeader(absoluteUrl);
      if (!string.IsNullOrWhiteSpace(cookieHeader))
      {
        requestBuilder.AddHeader(HeaderNames.Cookie, cookieHeader);
      }
    }
  }
}
