
namespace DSL_Tests
{
  using System;
  using System.Net;
  using U2U.AspNetCore.ScreenPlay;
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
