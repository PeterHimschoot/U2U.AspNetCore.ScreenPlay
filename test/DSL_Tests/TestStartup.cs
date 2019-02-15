namespace DSL_Tests
{
  using U2U.AspNetCore.ScreenPlay;
  using Core.Interfaces;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.DependencyInjection.Extensions;
  using WebSite;

  public class TestStartup : Startup
  {
    public TestStartup(IConfiguration configuration) : base(configuration) { }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);
      // Replace important dependencies for tests...
      services.ReplaceSingleton<IToDoRepository, FakeToDoRepository>();
    }
  }
}
