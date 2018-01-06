namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net;

  public class ShouldHaveApiStatusCode : ApiQuestion
  {
    private HttpStatusCode code;

    public ShouldHaveApiStatusCode(HttpStatusCode code) => this.code = code;

    protected override ApiClient Assert(ApiClient client)
    {
      Xunit.Assert.Equal(code, client.Response.StatusCode);
      return client;
    }
  }
}
