namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Security.Claims;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.Net.Http.Headers;
  using U2U.AspNetCore.ScreenPlay.Identity;

  public static class HttpClientExtensions
  {
    public static IHttpClient WithFakeClaimsPrincipal(this IHttpClient client, ClaimsPrincipal principal)
    => client.AddExtension(new FakeClaimsHttpClientExtension(principal));
    
    // => client.AddRequestExtension((requestBuilder, absoluteUri) =>
    //    {
    //      try
    //      {
    //        var ticket = new AuthenticationTicket(principal, "FakeClaims");
    //        var ser = new ClaimsSerializer();
    //        var cookieValue = ser.SerializeTicket(ticket);
    //        var cookieContainer = new CookieContainer();
    //        var cookie = new Cookie("FakeClaims", cookieValue, "/", absoluteUri.Authority);
    //        cookieContainer.Add(cookie);
    //        var cookieHeader = cookieContainer.GetCookieHeader(absoluteUri);
    //        requestBuilder.AddHeader(HeaderNames.Cookie, cookieHeader);
    //      }
    //      catch (Exception ex)
    //      {
    //        string message = ex.Message;
    //      }
    //    });
  }
}
