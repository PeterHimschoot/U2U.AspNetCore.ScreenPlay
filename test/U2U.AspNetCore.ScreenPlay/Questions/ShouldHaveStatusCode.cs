using System;
using System.Net;

namespace U2U.AspNetCore.ScreenPlay
{
  public class ShouldHaveStatusCode : BaseShouldHaveStatusCode, IQuestion
  {
    public ShouldHaveStatusCode(HttpStatusCode code, Uri location = null)
    : base(code, location) {}

    public virtual IHttpClient Assert(IHttpClient client)
    => (Browser) base.Assert( (Browser) client);
  }
}
