namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net;

  public class ShouldHaveApiCreatedStatusCode : BaseShouldHaveStatusCode, IApiQuestion
  {
    public ShouldHaveApiCreatedStatusCode(Uri location = null)
    :base(HttpStatusCode.Created, location) {} // => this.code = code;

    public virtual IHttpClient Assert(IHttpClient client)
    => (ApiClient) base.Assert((ApiClient) client);
  }
}
