namespace DSL_Tests
{
  using System;
  using System.Net;
  using U2U.AspNetCore.ScreenPlay;
  using Xunit;

  public class ShouldHaveHtmlHeader : Question
  {
    private string header;
    private string[] classes;

    public ShouldHaveHtmlHeader(string header, params string[] classes)
    {
      this.header = header ?? throw new ArgumentNullException(nameof(header));
      this.classes = classes;
    }

    protected override Browser Assert(Browser browser)
    {
      string cssQuery = Html.H1;
      this.classes?.ForEach(c => cssQuery += c);
      browser.DOM.Should().Contain(cssQuery, this.header);
      return browser;
    }
  }
}


