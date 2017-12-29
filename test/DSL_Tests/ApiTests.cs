namespace DSL_Tests
{
  using System;
  using Xunit;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.TestHost;
  using WebSite;
  using System.Net.Http;
  using System.Threading.Tasks;
  using System.Linq;
  using System.Net;
  using System.Reflection;
  using Microsoft.AspNetCore;
  using U2U.AspNetCore.ScreenPlay;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.DependencyInjection.Extensions;
  using Microsoft.Extensions.Configuration;
  using Core.Interfaces;
  using Xunit.Abstractions;
  using AngleSharp.Parser.Html;
  using AngleSharp.Dom.Html;
  using Core.Entities;
  using System.Net.Http.Headers;
  using System.IO;
  using WebSite.ViewModels.ToDo;
  using System.Collections.Generic;
  using System.Security.Claims;
  
  public class ApiTests
  {

    private ITestOutputHelper logger;

    public ApiTests(ITestOutputHelper logger)
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
    
    [Fact]
    public async Task ShowClaimsShouldReturnFakeClaims()
    {
      var claimSeeds = new Dictionary<string, string> {
        { "Claims", "list" }, { "Z", "A" }
      };
      var claimStrings =
        claimSeeds.Select(item => $"ClaimType = {item.Key}, Value = {item.Value}");

      var fakeIdentity = new ClaimsIdentity("FakeIdentity");
      foreach (var seed in claimSeeds)
      {
        fakeIdentity.AddClaim(new Claim(type: seed.Key, value: seed.Value));
      }
      Assert.True(fakeIdentity.IsAuthenticated);
      var fakePrincipal = new ClaimsPrincipal(fakeIdentity);
      
      var apiClient = Web.ApiClient<TestStartup>()
                       .WithAcceptJsonHeader()
                       .WithFakeClaimsPrincipal(fakePrincipal);  
      var peter = Actor.Named("Peter").CanUse(apiClient)
                       .And().CanUse<IToDoRepository>();
      await Given.That(peter).HasToDoItems(TestData.InitialToDos)
                 .And().CouldGet(Uris.ApiToDos).Successfully();
      peter.ApiClient().ShouldReturn().HaveStatusCode(HttpStatusCode.OK)
           .And().Should().HaveToDos(TestData.InitialToDos);
    }
  }
}
