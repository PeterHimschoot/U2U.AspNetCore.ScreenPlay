using System;
using System.Net;

namespace U2U.AspNetCore.ScreenPlay
{
  public class BaseShouldHaveStatusCode
  {
    private HttpStatusCode code;
    private Uri location;

    public BaseShouldHaveStatusCode(HttpStatusCode code, Uri location = null)
    {
      this.code = code;
      if (location != null && location.IsAbsoluteUri)
      {
        throw new ArgumentException("Please use a relative uri", nameof(location));
      }
      this.location = location;
    }

    protected virtual HttpClient Assert(HttpClient client)
    {
      Xunit.Assert.Equal(code, client.Response.StatusCode);
      if (location != null)
      {
        string expected = location.ToString();
        string actual = client.Response.Headers.Location.ToString();
        Xunit.Assert.Equal(expected, actual, ignoreCase: true);
      }
      return client;
    }
  }
}
