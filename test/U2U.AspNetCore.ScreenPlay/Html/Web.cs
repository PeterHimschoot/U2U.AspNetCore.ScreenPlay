namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.IO;
  using System.Reflection;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Configuration;

  public static class Web
  {
    // public static IAbility Browser(TestServer server) => new Browser(server);

    public static string ContentRoot { get; set; }

    public static Action<WebHostBuilderContext, IConfigurationBuilder> Configuration { get; set; }
    
    static Web()
    {
      ContentRoot = Path.Combine(Directory.GetCurrentDirectory(), "../../src/WebApp");
    }


    public static IAbility Browser<S>(Action<IServiceCollection> configureServices) where S : class
    {
      try
      {
        IConfiguration configuration = default(IConfiguration);
        S app = default(S);

        var webHostBuilder = new WebHostBuilder()
        .UseContentRoot(ContentRoot)
        .UseEnvironment(EnvironmentName.Development)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
          if (Configuration != null)
          {
            Configuration(hostingContext, config);
          }
          configuration = config.Build();
        })
        .ConfigureServices(services =>
        {
          app = (S)Activator.CreateInstance(typeof(S), configuration);
          dynamic d = app;
          d.ConfigureServices(services);
          configureServices(services);
        })
        .Configure(builder =>
        {
          IHostingEnvironment env = builder.ApplicationServices.GetService<IHostingEnvironment>();
          dynamic d = app;
          d.Configure(builder, env);
        });
        var testServer = new TestServer(webHostBuilder);
        return new Browser(testServer);
      }
      catch (Exception ex)
      {
        var msg = ex.Message;
        return null;
      }
    }

    public static IAbility Browser<S>() where S : class
    {
      try
      {
        var webHostBuilder = new WebHostBuilder()
        .UseContentRoot(ContentRoot)
        .UseEnvironment(EnvironmentName.Development)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
          if (Configuration != null)
          {
            Configuration(hostingContext, config);
          }
        })
        .UseStartup<S>();
        var testServer = new TestServer(webHostBuilder);
        return new Browser(testServer);
      }
      catch (Exception ex)
      {
        var msg = ex.Message;
        return null;
      }
    }
  }
}
