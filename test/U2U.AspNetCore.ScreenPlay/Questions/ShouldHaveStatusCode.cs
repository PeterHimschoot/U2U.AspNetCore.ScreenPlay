using System;
using System.Net;

namespace U2U.AspNetCore.ScreenPlay
{
  public class ShouldHaveStatusCode : Question
  {
    private HttpStatusCode code;
    private Uri location;

    public ShouldHaveStatusCode(HttpStatusCode code, Uri location = null)
    {
      this.code = code;
      if (location != null && location.IsAbsoluteUri)
      {
        throw new ArgumentException("Please use a relative uri", nameof(location));
      }
      this.location = location;
    }

    protected override Browser Assert(Browser browser)
    {
      Xunit.Assert.Equal(code, browser.Response.StatusCode);
      if (location != null)
      {
        string expected = location.ToString();
        string actual = browser.Response.Headers.Location.ToString();
        Xunit.Assert.Equal(expected, actual, ignoreCase: true);
      }
      return browser;
    }
  }
}
