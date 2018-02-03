using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;
using U2U.AspNetCore.ScreenPlay.Identity;

namespace U2U.AspNetCore.ScreenPlay
{
  public class FakeClaimsHttpClientExtension : IHttpClientExtension
  {
    ClaimsPrincipal principal;

    public FakeClaimsHttpClientExtension(ClaimsPrincipal principal)
    {
      this.principal = principal ?? throw new ArgumentNullException(nameof(principal));
    }

    public void BeforeRequest(RequestBuilder requestBuilder, Uri absoluteUrl)
    {
      var ticket = new AuthenticationTicket(principal, "FakeClaims");
      var ser = new ClaimsSerializer();
      var cookieValue = ser.SerializeTicket(ticket);
      var cookieContainer = new CookieContainer();
      var cookie = new Cookie("FakeClaims", cookieValue, "/", absoluteUrl.Authority);
      cookieContainer.Add(cookie);
      var cookieHeader = cookieContainer.GetCookieHeader(absoluteUrl);
      requestBuilder.AddHeader(HeaderNames.Cookie, cookieHeader);
    }

    public void AfterResponse(HttpResponseMessage response, Uri absoluteUrl) { }

  }
}
