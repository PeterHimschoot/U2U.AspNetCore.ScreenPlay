namespace U2U.AspNetCore.ScreenPlay
{
  using Core.Entities;
  using U2U.AspNetCore.ScreenPlay;
  using WebSite.ViewModels.ToDo;

  public static class TestTaskApiExtensions
  {
    public static TestTask CouldInsertToDoItem(this TestTask task, ToDoItem item)
    {
      return task.AddAction(new ApiPostToDoItem(item));
    }
  }
}
