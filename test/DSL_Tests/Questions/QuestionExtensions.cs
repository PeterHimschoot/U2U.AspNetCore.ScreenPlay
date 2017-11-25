namespace DSL_Tests
{
  using System.Net;
  using Core.Entities;
  using Core.Interfaces;
  using U2U.AspNetCore.ScreenPlay;

  public static class QuestionExtensions
  {
    public static Questions And(this Questions questions) => questions;
    
    public static Questions HaveHeader(this Questions q, string header)
    => q.Add(new ShouldHaveHeader(header));

    public static Questions HaveToDoItems(this Questions q, params string[] items)
    => q.Add(new ShouldHaveToDoItems(items));

    public static Questions HaveStatusCode(this Questions q, HttpStatusCode code)
    => q.Add(new ShouldHaveStatusCode(code));
    
    public static Questions AddedToDoItem(this Questions questions, ToDoItem item, IToDoRepository to)
    => questions.Add(new ShouldAddToDoItemToRepo(item, to));
  }
}
