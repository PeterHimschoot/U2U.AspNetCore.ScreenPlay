namespace U2U.AspNetCore.ScreenPlay {
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Security.Claims;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.Net.Http.Headers;

  public static class BrowserExtensions {
    
    public static Browser WithFakeClaims(this Browser browser, IEnumerable<Claim> claims ) {
      browser.AddRequestExtension( (requestBuilder, absoluteUri) => 
        {
          try {
          var cookieContainer = new CookieContainer();
          var cookieValue = "testing";
          var cookie = new Cookie("FakeClaims", cookieValue, "/", absoluteUri.Authority);
          cookieContainer.Add(cookie);
          var cookieHeader = cookieContainer.GetCookieHeader(absoluteUri);
          requestBuilder.AddHeader(HeaderNames.Cookie, cookieHeader);
          } catch(Exception ex) {
            string message = ex.Message;
          }
        });
        return browser;
    }
  }
}
