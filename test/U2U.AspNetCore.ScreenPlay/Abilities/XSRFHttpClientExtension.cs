using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;

namespace U2U.AspNetCore.ScreenPlay
{
  public class XSRFHttpClientExtension : CookiesHttpClientExtension
  {
    public string XSRFCookieName { get; set; } = ".AspNetCore.Antiforgery.ch-d0mA9hbw";
    public string XSRFHeaderName { get; set; } = "__RequestVerificationToken";
    public override void BeforeRequest(RequestBuilder requestBuilder, Uri absoluteUrl)
    {
      AddCookies(requestBuilder, absoluteUrl);
      SetXSRFHeader(requestBuilder, absoluteUrl);
    }

    public override void AfterResponse(HttpResponseMessage response, Uri absoluteUrl)
    {
      UpdateCookies(response, absoluteUrl);
    }
    
    private void SetXSRFHeader(RequestBuilder requestBuilder, Uri absoluteUrl)
    {
      var cookies = Cookies.GetCookies(absoluteUrl);
      var cookie = cookies[XSRFCookieName];
      if (cookie != null)
      {
        requestBuilder.AddHeader(XSRFHeaderName, cookie.Value);
      }
    }
  }
}
