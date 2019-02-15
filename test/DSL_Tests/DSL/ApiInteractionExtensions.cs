namespace U2U.AspNetCore.ScreenPlay
{
  using Core.Entities;
  using U2U.AspNetCore.ScreenPlay;
  using WebSite.ViewModels.ToDo;

  public static class ApiInteractionExtensions
  {
    public static Interaction CouldInsertToDoItem(this Interaction task, ToDoItem item)
    {
      return task.Add(new ApiPostToDoItem(item));
    }
  }
}
