using Core.Entities;
using Core.Interfaces;
using U2U.AspNetCore.ScreenPlay;

namespace DSL_Tests
{
  public static class APIQuestionExtensions
  {
    public static ApiQuestions HaveToDos(this ApiQuestions questions, ToDoItem[] todos)
    => questions.HaveResult<ToDoItem[]>(todos, new ArrayComparer<ToDoItem>(EntityBase.DefaultComparer));

    public static ApiQuestions CallCommit(this ApiQuestions questions, IToDoRepository repo)
    => questions.Add(new ApiShouldCallCommit(repo));
  }
}
