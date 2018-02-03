using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;

namespace U2U.AspNetCore.ScreenPlay {
  public interface IHttpClientExtension {
    void BeforeRequest(RequestBuilder requestBuilder, Uri absoluteUrl);
    
    void AfterResponse(HttpResponseMessage response, Uri absoluteUrl);
  }
}
