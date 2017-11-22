namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;
  using AngleSharp.Dom.Html;
  using AngleSharp.Parser.Html;
  using Microsoft.AspNetCore.TestHost;

  public class Browser : IAbility
  {
    string IAbility.Name { get; } = "Browser";

    public TestServer Server { get; }

    internal Browser(TestServer server)
    {
      this.Server = server ?? throw new ArgumentNullException(nameof(server));
    }

    public async Task ToOpenPageAsync(string uri)
    {
      var client = Server.CreateClient();
      await SetResponse(await client.GetAsync(uri));
    }

    private HttpResponseMessage response;
    
    private DOM dom;

    public async Task SetResponse(HttpResponseMessage response)
    {
      this.response = response;
      var parser = new HtmlParser();
      var document = await parser.ParseAsync(await response.Content.ReadAsStreamAsync());
      this.dom = new DOM(document);
    }

    public DOM DOM => this.dom;

    public Questions Should() => new Questions(dom);

    public RequestBuilder CreateRequest(string uri)
    => this.Server.CreateRequest(uri);
  }
}
