namespace U2U.AspNetCore.ScreenPlay
{
  using Microsoft.AspNetCore.TestHost;

  public interface IHttpClient : IAbility
  {
    TestServer Server { get; }
  }
}
