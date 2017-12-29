
namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Net.Http.Headers;

  public abstract class HttpClient : IAbility
  {
    public virtual string Name => "HttpClient";

    public TestServer Server { get; }

    protected HttpClient(TestServer server)
    {
      this.Server = server ?? throw new ArgumentNullException(nameof(server));
      this.AddRequestExtension(AddCookies);
    }

    private readonly List<Action<RequestBuilder, Uri>> extensions = new List<Action<RequestBuilder, Uri>>();

    public HttpClient AddRequestExtension(Action<RequestBuilder, Uri> extension)
    {
      extensions.Add(extension);
      return this;
    }

    protected void RunExtensions(RequestBuilder requestBuilder, Uri absoluteUri)
    => extensions.ForEach(extension => extension(requestBuilder, absoluteUri));

    public async Task GetAsync(string uri)
    {
      var fullUri = new Uri(this.Server.BaseAddress, uri);
      var absoluteUri = fullUri.AbsoluteUri;
      var requestBuilder = this.Server.CreateRequest(absoluteUri);
      RunExtensions(requestBuilder, fullUri);
      var response = await requestBuilder.GetAsync();
      await SetResponseAsync(response, fullUri);
    }

    public async Task PostAsync(string uri, string body)
    {
      var absoluteUrl = new Uri(this.Server.BaseAddress, uri);
      var requestBuilder = this.Server.CreateRequest(absoluteUrl.ToString());
      RunExtensions(requestBuilder, absoluteUrl);
      var response = await requestBuilder
        .And(message => message.Content = new StringContent(content: body))
        .PostAsync();
      await SetResponseAsync(response, absoluteUrl);
    }

    public async Task PutAsync(string method, string uri, string body)
    => await SendAsync("PUT", uri, body);

    public async Task DeleteAsync(string method, string uri, string body)
    => await SendAsync("DELETE", uri, body);

    public async Task SendAsync(string method, string uri, string body)
    {
      var absoluteUrl = new Uri(this.Server.BaseAddress, uri);
      var requestBuilder = this.Server.CreateRequest(absoluteUrl.ToString());
      RunExtensions(requestBuilder, absoluteUrl);
      var response = await requestBuilder
        .And(message => message.Content = new StringContent(content: body))
        .SendAsync(method);
      await SetResponseAsync(response, absoluteUrl);
    }

    public HttpResponseMessage Response { get; set; }

    protected async Task SetResponseAsync(HttpResponseMessage response, Uri absoluteUrl)
    {
      this.Response = response;
      UpdateCookies(response, absoluteUrl);
      await ProcessResponseAsync();
    }
    protected abstract Task ProcessResponseAsync();

    protected CookieContainer Cookies { get; } = new CookieContainer();

    protected void UpdateCookies(HttpResponseMessage response, Uri absoluteUrl)
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

    protected void AddCookies(RequestBuilder requestBuilder, Uri absoluteUrl)
    {
      var cookieHeader = Cookies.GetCookieHeader(absoluteUrl);
      if (!string.IsNullOrWhiteSpace(cookieHeader))
      {
        requestBuilder.AddHeader(HeaderNames.Cookie, cookieHeader);
      }
    }

    public IAbility And() => this;
  }
}
