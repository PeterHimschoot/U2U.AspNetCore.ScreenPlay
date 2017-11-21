namespace DSL_Tests
{
  using U2U.AspNetCore.ScreenPlay;

  public static class QuestionExtensions
  {
    public static Questions HaveHeader(this Questions q, string header)
      => q.Add(new ShouldHaveHeader(header));
      
      public static Questions HaveToDoItems(this Questions q, params string[] items)
      => q.Add(new ShouldHaveToDoItems(items));
  }
}
