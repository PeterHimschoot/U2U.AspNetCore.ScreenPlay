namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;
  using CsQuery;
  // using HtmlAgilityPack;
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
    private CQ dom;

    public async Task SetResponse(HttpResponseMessage response)
    {
      this.response = response;
      this.dom = CQ.CreateDocument(await response.Content.ReadAsStreamAsync());
    }

    public CQ DOM => dom;

    public Questions Should() => new Questions(new DOM(dom));

    public RequestBuilder CreateRequest(string uri)
    => this.Server.CreateRequest(uri);
  }
}
