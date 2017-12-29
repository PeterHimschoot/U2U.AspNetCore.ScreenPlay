namespace DSL_Tests {
  
  using Core.Entities;
  using U2U.AspNetCore.ScreenPlay;
  
  public static class APIQuestionExtensions {
    public static ApiQuestions HaveToDos(this ApiQuestions questions, ToDoItem[] todos ) 
    => questions.HaveResult<ToDoItem[]>(todos, new ArrayComparer<ToDoItem>(EntityBase.DefaultComparer));
    
  }
}
