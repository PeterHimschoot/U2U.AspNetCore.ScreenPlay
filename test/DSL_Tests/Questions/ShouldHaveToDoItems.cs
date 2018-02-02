
namespace DSL_Tests
{
  using System;
  using System.Net;
  using U2U.AspNetCore.ScreenPlay;
  using Xunit;

  public class ShouldHaveToDoItems : Question
  {
    private string[] items;

    public ShouldHaveToDoItems(params string[] items)
    {
      this.items = items ?? throw new ArgumentNullException(nameof(items));
    }

    protected override Browser Assert(Browser browser)
    {
      browser.DOM.Should().Contain($"{Html.Ul}#todolist>li", this.items);
      return browser;
    }
  }
}


