using U2U.AspNetCore.ScreenPlay;

namespace DSL_Tests
{
  public class ShouldHaveHeader : IQuestion
  {
    private string header;
    public ShouldHaveHeader(string header)
    {
      this.header = header;
    }

    DOM IQuestion.Assert(DOM dom)
    {
      // dom.Should().Contain(Html.H1).WithContents("Microsoft");
      return dom;
    }
  }

  public class ShouldHaveToDoItems : IQuestion
  {
    private string[] items;
    
    public ShouldHaveToDoItems(params string[] items)
    {
      this.items = items;
    }

    DOM IQuestion.Assert(DOM dom)
    {
      dom.Should().Contain("ul>li", this.items);
        //  .ContainSingle(Html.Ul, @class: "todolist")
        //  .WithAChild( element => { element.WithContentsIn(this.items); });
      
      return dom;
    }
  }
}
