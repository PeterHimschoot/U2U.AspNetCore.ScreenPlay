namespace U2U.AspNetCore.ScreenPlay
{
  using System.Net.Http.Headers;
  using Xunit;

  public class ShouldHaveContentType : IQuestion
  {
    private MediaTypeHeaderValue contentType;

    public ShouldHaveContentType(string contentType)
    : this(MediaTypeHeaderValue.Parse(contentType)) { }

    public ShouldHaveContentType(MediaTypeHeaderValue contentType)
    => this.contentType = contentType;

    Browser IQuestion.Assert(Browser browser)
    {
      Assert.Equal(contentType, browser.Response.Content.Headers.ContentType);
      return browser;
    }
  }
}
