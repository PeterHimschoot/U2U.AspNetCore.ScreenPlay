namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net;

  public class ShouldHaveStatusCode : Question
  {
    private HttpStatusCode code;

    public ShouldHaveStatusCode(HttpStatusCode code) 
    => this.code = code;

    protected override Browser Assert(Browser browser)
    {
      Xunit.Assert.Equal(code, browser.Response.StatusCode);
      return browser;
    }
  }
}
