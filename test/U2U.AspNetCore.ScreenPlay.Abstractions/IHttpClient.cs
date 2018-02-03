using System;
using Microsoft.AspNetCore.TestHost;

namespace U2U.AspNetCore.ScreenPlay
{
  public interface IHttpClient : IAbility
  {
    TestServer Server { get; }
    
    IHttpClient AddExtension(IHttpClientExtension extension);
  }
}
