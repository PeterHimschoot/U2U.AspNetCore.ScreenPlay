using System;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using WebSite;
using System.Net.Http;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore;
using U2U.AspNetCore.ScreenPlay;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.Interfaces;
using Xunit.Abstractions;

namespace DSL_Tests
{
  public class ToDoItemsTests
  {
    private ITestOutputHelper logger;
    private TestServer server;
    private HttpClient client;
    private FakeToDoRepository repository;

    public ToDoItemsTests(ITestOutputHelper logger)
    {
      this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
      
      var builder = WebHost.CreateDefaultBuilder()
      .UseStartup<TestStartup>()
      .UseContentRoot("/Users/peterhimschoot/Documents/Code/NET_CORE/DSL_Testing/src/WebSite");

      // builder.ConfigureServices((IServiceCollection services) =>
      // {
      //   // Replace the normal IToDoRepository with face to have more control over tests
      //   var descriptor = new ServiceDescriptor(typeof(IToDoRepository), repository);
      //   services.Replace(descriptor);
      // });

      server = new TestServer(builder);
      
      repository = (FakeToDoRepository)  server.Host.Services.GetRequiredService(typeof(IToDoRepository));
      
      client = server.CreateClient();
    }

    [Fact]
    public async Task IndexShouldContainMicrosoftHeading()
    {
      var response = await client.GetAsync("/");
      // Assert.Equal(response.StatusCode, HttpStatusCode.OK);

      var cwd = Environment.CurrentDirectory;

      var html = await response.Content.ReadAsStringAsync();
      var doc = new HtmlDocument();
      doc.LoadHtml(html);

      var headers = doc.DocumentNode.Descendants("h1").Where(h => h.InnerText.Contains("Microsoft"));
      Assert.True(headers.Count() >= 1);
    }

    [Fact]
    public async Task UserOpensDefaultPage()
    {

      var peter = Actor.Named("Peter").CanUse(Web.Browser(server));
      await new Given(peter).CouldGoToDefaultPage().Successfully();
      peter.Browser().Should().HaveHeader("Microsoft");
    }

    // [Fact]
    // public async Task UserAddsANewToDoItem()
    // {
    //   // var peter = Actor.Named("Peter").CanUse(Web.Browser(server));
    //   // await new Given(peter)
    // }

    [Fact()]
    public async Task AUserShouldSeeOnlyTheirTasks()
    {
      var peter = Actor.Named("Peter").CanUse(Web.Browser(server)).And().CanUse(this.repository);

      await new Given(peter)
      .HasToDoItems("Make coffee", "Feed the cat")
      .And().CouldGoToItemsPage().Successfully();
      
      logger.WriteLine("***Made it here***");

      peter.Browser().Should().HaveToDoItems("Make coffee", "Feed the cat");
    }
  }
}
