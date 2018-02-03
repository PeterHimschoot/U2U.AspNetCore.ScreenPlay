using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace U2U.AspNetCore.ScreenPlay
{

  public class Browser : HttpClient // IAbility
  {
    public static Action<IServiceCollection> WithDefaults = s => { };

    override public string Name => "Browser";

    internal Browser(TestServer server)
    : base(server) { }

    public async Task ToOpenPageAsync(string uri)
    => await GetAsync(uri);

    public async Task PostToControllerAsync(string uri, FormValues form)
    {
      var absoluteUrl = new Uri(this.Server.BaseAddress, uri);
      var requestBuilder = this.Server.CreateRequest(absoluteUrl.ToString());
      Extensions.RunBeforeRequest(requestBuilder, absoluteUrl);
      // RunExtensions(requestBuilder, absoluteUrl);
      // AddCookies(requestBuilder, absoluteUrl);
      // SetXSRFHeader(requestBuilder, absoluteUrl);
      form.Add(VerificationToken.Name, this.verificationToken.Token);
      var content = new FormUrlEncodedContent(form.Values);
      var response = await requestBuilder.And(message =>
      {
        message.Content = content;
      }).PostAsync();
      await SetResponseAsync(response, absoluteUrl);
    }

    private DOM dom;

    private VerificationToken verificationToken;

    protected override async Task ProcessResponseAsync()
    {
      var parser = new HtmlParser();
      var content = await Response.Content.ReadAsStringAsync();
      var document = await parser.ParseAsync(content);
      verificationToken = VerificationToken.From(document);
      this.dom = new DOM(document);
    }

    // public string XSRFCookieName { get; set; } = ".AspNetCore.Antiforgery.ch-d0mA9hbw";
    // public string XSRFHeaderName { get; set; } = "__RequestVerificationToken";

    // private void SetXSRFHeader(RequestBuilder requestBuilder, Uri absoluteUrl)
    // {
    //   var cookies = Cookies.GetCookies(absoluteUrl);
    //   var cookie = cookies[XSRFCookieName];
    //   if (cookie != null)
    //   {
    //     requestBuilder.AddHeader(XSRFHeaderName, cookie.Value);
    //   }
    // }

    public DOM DOM => this.dom;

    public Questions Should() => new Questions(this);

    // public RequestBuilder CreateRequest(string uri)
    // => this.Server.CreateRequest(uri);





  }
}
