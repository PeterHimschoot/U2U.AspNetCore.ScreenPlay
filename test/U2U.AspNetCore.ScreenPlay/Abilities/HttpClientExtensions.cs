namespace U2U.AspNetCore.ScreenPlay {
  
  using System;
  using Microsoft.Net.Http.Headers;

  public static class HttpClientExtensions {
    
    public static HttpClient AddHeaderExtension(this HttpClient client, string headerName, string headerValue)
    => client.AddRequestExtension( (requistBuilder, absoluteUri) => {
        requistBuilder.AddHeader(headerName, headerValue);
      });
      
    public static HttpClient WithAcceptJsonHeader(this HttpClient client)
    => client.AddHeaderExtension(HeaderNames.Accept, "application/json");  
  }
}
