namespace DSL_Tests
{
  using System;
  using System.IO;
  using System.Reflection;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using U2U.AspNetCore.ScreenPlay;
  using Xunit.Abstractions;

  public class DSLTest
  {
    private ITestOutputHelper logger;
    public DSLTest(ITestOutputHelper logger)
    {
      this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
      string projectRoot = Path.Combine(Environment.CurrentDirectory, "../../../../..");
      Web.ContentRoot = Path.Combine(projectRoot, "src/WebSite");
      Web.Configuration = (hostingContext, config) =>
      {
        var env = hostingContext.HostingEnvironment;

        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

        if (env.IsDevelopment())
        {
          var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
          if (appAssembly != null)
          {
            config.AddUserSecrets(appAssembly, optional: true);
          }
        }
        config.AddEnvironmentVariables();
      };
    }
  }
}
