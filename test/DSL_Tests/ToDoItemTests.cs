using System;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using WebSite;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore;
using U2U.AspNetCore.ScreenPlay;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.Interfaces;
using Xunit.Abstractions;
using AngleSharp.Parser.Html;
using AngleSharp.Dom.Html;
using Core.Entities;
using webSite.Pages;
using webSite;

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

      server = new TestServer(builder);
      repository = (FakeToDoRepository)server.Host.Services.GetRequiredService(typeof(IToDoRepository));
      client = server.CreateClient();
    }

    [Fact]
    public async Task IndexShouldContainMicrosoftHeading()
    {
      var response = await client.GetAsync("/");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      var parser = new HtmlParser();
      var document = await parser.ParseAsync(await response.Content.ReadAsStreamAsync());
      var companyHeader = document.QuerySelector("h1#company");
      Assert.Contains("Microsoft", companyHeader.InnerHtml);
    }

    [Fact]
    public async Task CreateNewItemShouldInsertIntoDatabase()
    {
      var createPage = await client.GetAsync("/Create");
      var model = new CreateViewModel()
      {
        Item = new ToDoItem
        {
          Title = "Eat cat",
          Description = "When really hungry",
          DeadLine = DateTime.Now.AddDays(100)
        }
      };
      var fb = await new FormBuilder()
                 .Add(x => model.Item.Title)
                 .Add(x => model.Item.Description)
                 .CopyCSRFToken(createPage)
                 ;
                 
      var content = fb.Create();

      var response = await client.PostAsync("/Create", content);

      var contents = await response.Content.ReadAsStringAsync();

      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }

    [Fact]
    public async Task UserOpensDefaultPage()
    {
      // Arrange
      var peter = Actor.Named("Peter").CanUse(Web.Browser(server));
      // Act
      await new Given(peter).CouldGoToDefaultPage().Successfully();
      // Assert
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
      // Arrange
      var peter = Actor.Named("Peter").CanUse(Web.Browser(server)).And().CanUse(this.repository);
      // Act
      await new Given(peter).HasToDoItems("Make coffee", "Feed the cat").And().CouldGoToItemsPage().Successfully();
      // Assert
      peter.Browser().Should().HaveToDoItems("Make coffee", "Feed the cat");
    }
  }
}
