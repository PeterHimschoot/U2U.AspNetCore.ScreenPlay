namespace U2U.AspNetCore.ScreenPlay
{
  using System.Net.Http.Headers;

  public class ShouldHaveApiContentType : ApiQuestion
  {
    private MediaTypeHeaderValue contentType;

    public ShouldHaveApiContentType(string contentType)
    : this(MediaTypeHeaderValue.Parse(contentType)) { }

    public ShouldHaveApiContentType(MediaTypeHeaderValue contentType)
    => this.contentType = contentType;

    protected override ApiClient Assert(ApiClient client)
    {
      var expected = this.contentType;
      var actual = client.Response.Content.Headers.ContentType;
      var match = expected == actual;
      Xunit.Assert.True(match, userMessage: $"Expected return contentType to be {expected}, but got {actual} instead");
      return client;
    }
  }
}
