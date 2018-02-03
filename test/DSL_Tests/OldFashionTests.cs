using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using U2U.AspNetCore.ScreenPlay;
using Xunit;

namespace DSL_Tests
{

  public class OldFashionedTests
  {

    public TestServer Server { get; set; }

    public OldFashionedTests()
    {
    }
    
    [Fact]
    public async Task AUserShouldSeeOnlyTheirTasks()
    {
      var contentRoot = Path.Combine(Directory.GetCurrentDirectory(), "../../../../../src/WebSite");
      var webHostBuilder = new WebHostBuilder()
      .UseContentRoot(contentRoot)
      .UseEnvironment(EnvironmentName.Development)
      .ConfigureAppConfiguration((hostingContext, config) =>
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
      })
      .UseStartup<TestStartup>();
      Server = new TestServer(webHostBuilder);

      IToDoRepository repo =
        Server.Host.Services.GetService(typeof(IToDoRepository)) as IToDoRepository;

      TestData.InitialToDos.ForEach(item => repo.AddToDoItem(item));
      await repo.CommitAsync();

      var client = Server.CreateClient();
      var response = await client.GetAsync(Uris.Home);
      var content = await response.Content.ReadAsStringAsync();
      var parser = new HtmlParser();
      var document = await parser.ParseAsync(content);
      var listItems = document.QuerySelectorAll($"{Html.Ul}#todolist>li");

      var items = TestData.InitialToDos.Select(item => item.Title);
      foreach (var li in listItems)
      {
        Assert.Contains(li.InnerHtml, items);
      }
    }
  }
}
