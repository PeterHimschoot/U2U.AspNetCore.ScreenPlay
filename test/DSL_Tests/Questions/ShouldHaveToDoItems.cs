
namespace DSL_Tests
{
  using System;
  using System.Net;
  using U2U.AspNetCore.ScreenPlay;
  using Xunit;

  public class ShouldHaveToDoItems : IQuestion
  {
    private string[] items;

    public ShouldHaveToDoItems(params string[] items)
    {
      this.items = items ?? throw new ArgumentNullException(nameof(items));
    }

    Browser IQuestion.Assert(Browser browser)
    {
      browser.DOM.Should().Contain("ul#todolist>li", this.items);
      //  .ContainSingle(Html.Ul, @class: "todolist")
      //  .WithAChild( element => { element.WithContentsIn(this.items); });

      return browser;
    }
  }
}


