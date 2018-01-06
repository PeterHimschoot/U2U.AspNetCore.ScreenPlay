namespace U2U.AspNetCore.ScreenPlay {
  using System;
  using System.Collections.Generic;

  public class ShouldHaveContentsWith : Question {
    
    private string path;
    private string[] items;
    
    public ShouldHaveContentsWith(string path, params string[] items) {
      this.path = path ?? throw new ArgumentNullException(nameof(path));
      this.items = items ?? throw new ArgumentNullException(nameof(items));
    }

    protected override Browser Assert(Browser browser)
    {
      browser.DOM.Should().Contain(path, this.items);
      return browser;
    }
  }
}
