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
using webSite.Pages;
using webSite;
using System.Net.Http.Headers;
using System.IO;

namespace DSL_Tests
{

  // https://reasoncodeexample.com/2016/08/29/how-to-keep-things-tidy-when-using-asp-net-core-testserver/

  public class ToDoItemsTests
  {
    private ITestOutputHelper logger;
    // private TestServer server;
    // private HttpClient client;
    // public FakeToDoRepository Repository { get; }

    public ToDoItemsTests(ITestOutputHelper logger)
    {
      this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

      logger.WriteLine($"Current dir = {Environment.CurrentDirectory}");
      string projectRoot = Path.Combine(Environment.CurrentDirectory, "../../../../..");
      logger.WriteLine($"Project root = {projectRoot}");
      Web.ContentRoot = Path.Combine(projectRoot, "src/WebSite");
      logger.WriteLine($"Web.ContentRoot = {Web.ContentRoot}");
      
      // Web.ContentRoot = "http://localhost:5000";
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

      // var builder = WebHost.CreateDefaultBuilder()
      // .UseStartup<TestStartup>()
      // .UseContentRoot("/Users/peterhimschoot/Documents/Code/NET_CORE/DSL_Testing/src/WebSite");

      // server = new TestServer(builder);
      //  Repository = (FakeToDoRepository)server.Host.Services.GetRequiredService(typeof(IToDoRepository));
      // client = server.CreateClient();

      // client = new HttpClient();
      // client.BaseAddress = new Uri("http://localhost:5000/");
    }

    // [Fact]
    // public async Task IndexShouldContainMicrosoftHeading()
    // {
    //   var response = await client.GetAsync("/");
    //   Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //   var parser = new HtmlParser();
    //   var document = await parser.ParseAsync(await response.Content.ReadAsStreamAsync());
    //   var companyHeader = document.QuerySelector("h1#company");
    //   Assert.Contains("Microsoft", companyHeader.InnerHtml);
    // }

    // [Fact(Skip = "Obsolete")]
    // public async Task CreateNewItemShouldInsertIntoDatabase()
    // {
    //   var createPage = await client.GetAsync("/Create");
    //   var model = new CreateViewModel()
    //   {
    //     Item = new ToDoItem
    //     {
    //       Title = "Eat cat",
    //       Description = "When really hungry",
    //       DeadLine = DateTime.Now.AddDays(100)
    //     }
    //   };
    //   var fb = await new FormBuilder()
    //              .Add(x => model.Item.Title)
    //              .Add(x => model.Item.Description)
    //              .CopyCSRFToken(createPage)
    //              ;

    //   var content = fb.Create();
    //   var response = await client.PostAsync("/Create", content);
    //   var contents = await response.Content.ReadAsStringAsync();
    //   Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    // }

    [Fact]
    public async Task CreateNewItemShouldInsertIntoDatabaseToo()
    {
      var model = new CreateViewModel()
      {
        Item = new ToDoItem
        {
          Title = "Eat cat",
          Description = "When really hungry",
          DeadLine = DateTime.Now.AddDays(100)
        }
      };
      var peter = Actor.Named("Peter").CanUse(Web.Browser<TestStartup>())
                       .And().CanUse<IToDoRepository>();
      await Given.That(peter).CouldGoToItemsCreate().And()
                      .EnterNewToDo(model).Successfully();
      peter.Browser().Should().HaveStatusCode(HttpStatusCode.Redirect)
           .And().AddedToDoItem(model.Item, peter.Ability<IToDoRepository>());
    }

    [Fact]
    public async Task UserOpensDefaultPage()
    {
      // Arrange
      var peter = Actor.Named("Peter").CanUse(Web.Browser<Startup>());
      // Act
      await Given.That(peter).CouldGoToDefaultPage().Successfully();
      // Assert
      peter.Browser().Should().HaveHtmlHeader("Microsoft")
           .And().Should().HaveContentType("text/html; charset=utf-8");
          //  .And().Should().HaveHeader("Cache-Control", "no-cache, no-store");
    }

    [Fact()]
    public async Task AUserShouldSeeOnlyTheirTasks()
    {
      // Arrange
      var peter = Actor.Named("Peter").CanUse(Web.Browser<TestStartup>())
                       .And().CanUse<IToDoRepository>();
      // Act
      await Given.That(peter).HasToDoItems("Make coffee", "Feed the cat")
                 .And().CouldGoToItemsPage().Successfully();
      // Assert
      peter.Browser().Should().HaveToDoItems("Make coffee", "Feed the cat");
    }
  }
}
