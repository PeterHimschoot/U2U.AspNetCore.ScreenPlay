namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;

  public interface IQuestion
  {
    IHttpClient Assert(IHttpClient browser);
  }
}
