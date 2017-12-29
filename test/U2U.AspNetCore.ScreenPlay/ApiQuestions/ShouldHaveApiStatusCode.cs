namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net;
  using Xunit;

  public class ShouldHaveApiStatusCode : IApiQuestion
  {
    private HttpStatusCode code;

    public ShouldHaveApiStatusCode(HttpStatusCode code) => this.code = code;

    ApiClient IApiQuestion.Assert(ApiClient client)
    {
      Assert.Equal(code, client.Response.StatusCode);
      return client;
    }
  }
}
