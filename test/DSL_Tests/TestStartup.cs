namespace DSL_Tests
{
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
      ReplaceWithFakeServices(services);
    }
    
    private void ReplaceWithFakeServices(IServiceCollection services) {
      var descriptor = new ServiceDescriptor(typeof(IToDoRepository), typeof(FakeToDoRepository), ServiceLifetime.Singleton);
      services.Replace(descriptor);
    }
  }
}
