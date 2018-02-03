
namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Text;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Net.Http.Headers;

  public abstract class HttpClient : IHttpClient
  {
    public abstract string Name { get; }

    public virtual TestServer Server { get; }

    protected HttpClient(TestServer server)
    {
      this.Server = server ?? throw new ArgumentNullException(nameof(server));
      this.WithCookies();
    }

    public Encoding Encoding { get; set; } = Encoding.UTF8;

    public string MediaType { get; set; } = "application/json";
    
    public HttpClient WithMediaType(string mediaType) {
      this.MediaType = mediaType;
      return this;
    }

    // private readonly List<Action<RequestBuilder, Uri>> extensions = new List<Action<RequestBuilder, Uri>>();
    protected HttpClientExtensions Extensions { get; } = new HttpClientExtensions();

    public virtual IHttpClient AddExtension(IHttpClientExtension extension)
    {
      Extensions.Add(extension);
      return this;
    }

    // protected void RunExtensions(RequestBuilder requestBuilder, Uri absoluteUri)
    // => extensions.ForEach(extension => extension(requestBuilder, absoluteUri));

    public async Task GetAsync(string uri)
    {
      var absoluteUri = new Uri(this.Server.BaseAddress, uri);
      var requestBuilder = this.Server.CreateRequest(absoluteUri.AbsoluteUri);
      Extensions.RunBeforeRequest(requestBuilder, absoluteUri);
      // RunExtensions(requestBuilder, absoluteUrl);
      // RunExtensions(requestBuilder, fullUri);
      var response = await requestBuilder.GetAsync();
      await SetResponseAsync(response, absoluteUri);
    }

    public async Task PostAsync(string uri, string body)
    {
      var absoluteUrl = new Uri(this.Server.BaseAddress, uri);
      var requestBuilder = this.Server.CreateRequest(absoluteUrl.ToString());
      Extensions.RunBeforeRequest(requestBuilder, absoluteUrl);
      var response = await requestBuilder
        .And(message => message.Content = new StringContent(content: body, encoding: Encoding, mediaType: MediaType))
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
      Extensions.RunBeforeRequest(requestBuilder, absoluteUrl);
      var response = await requestBuilder
        .And(message => message.Content = new StringContent(content: body, encoding: Encoding, mediaType: MediaType))
        .SendAsync(method);
      await SetResponseAsync(response, absoluteUrl);
    }

    public HttpResponseMessage Response { get; set; }

    protected async Task SetResponseAsync(HttpResponseMessage response, Uri absoluteUrl)
    {
      this.Response = response;
      Extensions.RunAfterResponse(response, absoluteUrl);
      await ProcessResponseAsync();
    }

    protected abstract Task ProcessResponseAsync();

    public IAbility And() => this;

    protected class HttpClientExtensions
    {
      private List<IHttpClientExtension> extensions = new List<IHttpClientExtension>();

      public void Add(IHttpClientExtension extension)
      => extensions.Add(extension);

      public void RunBeforeRequest(RequestBuilder requestBuilder, Uri absoluteUrl)
        => extensions.ForEach(ext => ext.BeforeRequest(requestBuilder, absoluteUrl));

      public void RunAfterResponse(HttpResponseMessage response, Uri absoluteUrl)
      => extensions.ForEach(ext => ext.AfterResponse(response, absoluteUrl));
    }
  }
}
