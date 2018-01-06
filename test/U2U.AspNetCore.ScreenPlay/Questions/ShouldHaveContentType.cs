namespace U2U.AspNetCore.ScreenPlay
{
  using System.Net.Http.Headers;

  public class ShouldHaveContentType : Question
  {
    private MediaTypeHeaderValue contentType;

    public ShouldHaveContentType(string contentType)
    : this(MediaTypeHeaderValue.Parse(contentType)) { }

    public ShouldHaveContentType(MediaTypeHeaderValue contentType)
    => this.contentType = contentType;

    protected override Browser Assert(Browser browser)
    {
      Xunit.Assert.Equal(contentType, browser.Response.Content.Headers.ContentType);
      return browser;
    }
  }
}
