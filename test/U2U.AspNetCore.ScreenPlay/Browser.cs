namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;
  using HtmlAgilityPack;
  using Microsoft.AspNetCore.TestHost;

  public class Browser : Ability
  {
    public TestServer Server { get; }

    internal Browser(TestServer server)
    : base("Browser")
    {
      this.Server = server ?? throw new ArgumentNullException(nameof(server));
    }

    public async Task ToOpenPageAsync(string uri)
    {
      var client = Server.CreateClient();
      await SetResponse(await client.GetAsync(uri));
    }

    private HttpResponseMessage response;
    private HtmlDocument dom;

    public async Task SetResponse(HttpResponseMessage response)
    {
      this.response = response;

      this.dom = new HtmlDocument();
      this.dom.Load(await response.Content.ReadAsStreamAsync());
    }
    
    public HtmlDocument DOM => dom;

    public Questions Should() => new Questions(new DOM(dom));

  }
}
