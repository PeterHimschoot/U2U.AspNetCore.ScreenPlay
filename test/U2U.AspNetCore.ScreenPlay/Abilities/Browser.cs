namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using AngleSharp.Dom.Html;
  using AngleSharp.Parser.Html;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Net.Http.Headers;

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
      var absoluteUrl = new Uri(this.Server.BaseAddress, uri);
      await SetResponse(await client.GetAsync(uri), absoluteUrl);
    }

    public async Task PostToControllerAsync(string uri, FormValues form)
    {
      var absoluteUrl = new Uri(this.Server.BaseAddress, uri);
      var requestBuilder = this.Server.CreateRequest(absoluteUrl.ToString());
      AddCookies(requestBuilder, absoluteUrl);
      SetXSRFHeader(requestBuilder, absoluteUrl);
      form.Add(VerificationToken.Name, this.verificationToken.Token);
      var content = new FormUrlEncodedContent(form.Values);
      var response = await requestBuilder.And(message =>
      {
        message.Content = content;
      }).PostAsync();
      await SetResponse(response, absoluteUrl);
    }


    public HttpResponseMessage Response { get; set; }

    private DOM dom;

    private VerificationToken verificationToken;

    public async Task SetResponse(HttpResponseMessage response, Uri absoluteUrl)
    {
      this.Response = response;
      UpdateCookies(response, absoluteUrl);
      var parser = new HtmlParser();
      var document = await parser.ParseAsync(await response.Content.ReadAsStreamAsync());
      verificationToken = VerificationToken.From(document);
      this.dom = new DOM(document);
    }

    private CookieContainer Cookies { get; } = new CookieContainer();

    private void UpdateCookies(HttpResponseMessage response, Uri absoluteUrl)
    {
      if (response.Headers.Contains(HeaderNames.SetCookie))
      {
        var cookies = response.Headers.GetValues(HeaderNames.SetCookie);
        foreach (var cookie in cookies)
        {
          Cookies.SetCookies(absoluteUrl, cookie);
        }
      }
    }

    private void AddCookies(RequestBuilder requestBuilder, Uri absoluteUrl)
    {
      var cookieHeader = Cookies.GetCookieHeader(absoluteUrl);
      if (!string.IsNullOrWhiteSpace(cookieHeader))
      {
        requestBuilder.AddHeader(HeaderNames.Cookie, cookieHeader);
      }
    }

    public string XSRFCookieName { get; set; } = ".AspNetCore.Antiforgery.ch-d0mA9hbw";
    public string XSRFHeaderName { get; set; } = "__RequestVerificationToken";

    private void SetXSRFHeader(RequestBuilder requestBuilder, Uri absoluteUrl)
    {
      var cookies = Cookies.GetCookies(absoluteUrl);
      var cookie = cookies[XSRFCookieName];
      if (cookie != null)
      {
        requestBuilder.AddHeader(XSRFHeaderName, cookie.Value);
      }
    }

    public DOM DOM => this.dom;

    public Questions Should() => new Questions(this);

    public RequestBuilder CreateRequest(string uri)
    => this.Server.CreateRequest(uri);





  }
}
