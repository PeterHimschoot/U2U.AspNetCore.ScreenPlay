namespace DSL_Tests
{
  using System;
  using System.Net;
  using U2U.AspNetCore.ScreenPlay;
  using Xunit;

  public class ShouldHaveHeader : IQuestion
  {
    private string header;

    public ShouldHaveHeader(string header)
    {
      this.header = header ?? throw new ArgumentNullException(nameof(header));
    }

    Browser IQuestion.Assert(Browser browser)
    {
      browser.DOM.Should().Contain(Html.H1, "Microsoft");
      return browser;
    }
  }
}


