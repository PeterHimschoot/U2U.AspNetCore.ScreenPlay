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

namespace DSL_Tests
{
  public class WebSiteTests
  {
    private TestServer server;
    private HttpClient client;

    public WebSiteTests()
    {
      var builder = WebHost.CreateDefaultBuilder()
      .UseStartup<Startup>()
      .UseContentRoot("/Users/peterhimschoot/Documents/Code/NET_CORE/DSL_Testing/src/WebSite");
      server = new TestServer(builder);
      
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
  }
}
