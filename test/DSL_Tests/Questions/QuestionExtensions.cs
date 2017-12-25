namespace DSL_Tests
{
  using System.Net;
  using Core.Entities;
  using Core.Interfaces;
  using U2U.AspNetCore.ScreenPlay;

  public static class QuestionExtensions
  {
    public static Questions HaveHtmlHeader(this Questions questions, string header)
    => questions.Add(new ShouldHaveHtmlHeader(header));

    public static Questions HaveToDoItems(this Questions questions, params string[] items)
    => questions.Add(new ShouldHaveToDoItems(items));
 
    public static Questions AddedToDoItem(this Questions questions, ToDoItem item, IToDoRepository to)
    => questions.Add(new ShouldAddToDoItemToRepo(item, to));
  }
}
