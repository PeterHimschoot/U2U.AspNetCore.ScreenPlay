namespace U2U.AspNetCore.ScreenPlay
{
  using Microsoft.AspNetCore.TestHost;
  
  public static class Web {
    public static IAbility Browser(TestServer server) => new Browser(server);
  }
}
