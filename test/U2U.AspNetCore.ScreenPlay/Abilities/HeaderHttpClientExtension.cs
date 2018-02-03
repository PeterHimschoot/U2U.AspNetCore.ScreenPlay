using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;

namespace U2U.AspNetCore.ScreenPlay
{
  public class HeaderHttpClientExtension : IHttpClientExtension
  {
    private string name;
    private string value;

    public HeaderHttpClientExtension(string name, string value)
    {
      this.name = name;
      this.value = value;
    }

    public void AfterResponse(HttpResponseMessage response, Uri absoluteUrl) { }

    public void BeforeRequest(RequestBuilder requestBuilder, Uri absoluteUrl)
    => requestBuilder.AddHeader(name, value);
  }
}
