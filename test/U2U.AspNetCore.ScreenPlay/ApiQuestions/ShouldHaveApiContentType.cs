namespace U2U.AspNetCore.ScreenPlay
{
  using System.Net.Http.Headers;
  using Xunit;

  public class ShouldHaveApiContentType : IApiQuestion
  {
    private MediaTypeHeaderValue contentType;

    public ShouldHaveApiContentType(string contentType)
    : this(MediaTypeHeaderValue.Parse(contentType)) { }

    public ShouldHaveApiContentType(MediaTypeHeaderValue contentType)
    => this.contentType = contentType;

    ApiClient IApiQuestion.Assert(ApiClient client)
    {
      var expected = this.contentType;
      var actual = client.Response.Content.Headers.ContentType;
      var match = expected == actual;
      Assert.True(match, userMessage: $"Expected return contentType to be {expected}, but got {actual} instead");
      return client;
    }
  }
}
