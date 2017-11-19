using U2U.AspNetCore.ScreenPlay;

namespace DSL_Tests
{

  public static class QuestionExtensions
  {
    public static Questions HaveHeader(this Questions q, string header)
      => q.Add(new ShouldHaveHeader(header));
  }

  public class ShouldHaveHeader : Question
  {
    private string header;
    public ShouldHaveHeader(string header)
    {
      this.header = header;
    }

    public override DOM Assert(DOM dom)
    {
      dom.ContainSingle(Html.H1).WithContents("Microsoft");
      return dom;
    }
  }
}
