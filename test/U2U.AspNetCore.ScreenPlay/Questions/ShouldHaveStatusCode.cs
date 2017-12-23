
namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net;
  using Xunit;

  public class ShouldHaveStatusCode : IQuestion
  {
    private HttpStatusCode code;

    public ShouldHaveStatusCode(HttpStatusCode code) => this.code = code;


    Browser IQuestion.Assert(Browser browser)
    {
      Assert.Equal(code, browser.Response.StatusCode);
      return browser;
    }
  }
}
