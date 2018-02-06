namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net;

  public class ShouldHaveApiStatusCode : BaseShouldHaveStatusCode, IApiQuestion
  {
    public ShouldHaveApiStatusCode(HttpStatusCode code, Uri location = null)
    :base(code, location) {}

    public virtual IHttpClient Assert(IHttpClient client)
    => (ApiClient) base.Assert((ApiClient) client);
  }
}
