using System;
using System.Collections.Generic;

namespace U2U.AspNetCore.ScreenPlay {
  public interface IApiQuestion {
    IHttpClient Assert(IHttpClient client);
  }
}
