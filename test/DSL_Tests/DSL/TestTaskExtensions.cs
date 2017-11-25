namespace DSL_Tests
{
  using U2U.AspNetCore.ScreenPlay;
  using webSite;

  public static class TestTaskExtensions
  {
    public static TestTask CouldGoToDefaultPage(this TestTask task)
      => task.AddAction(new Browses(Uris.Home));
    
    public static TestTask CouldGoToItemsPage(this TestTask task)
    => task.AddAction(new Browses(Uris.Items));
    
    public static TestTask HasToDoItems(this TestTask task, params string[] items) {
      foreach( var item in items) {
        task.AddAction( new AddInitialToDoItem(item));
      }
      return task;
    }
    
    public static TestTask CouldGoToItemsCreate(this TestTask task) {
      return task.AddAction(new Browses(Uris.Create));
    }
    
    public static TestTask EnterNewToDo(this TestTask task, CreateViewModel model) {
      return task.AddAction(new CreateToDoItem(model));
    }
  }
  
  // public static class BrowserExtensions {
  //   public static Browser ShouldFindProperTitle(this Browser browser)
  //   => browser.ShouldFind(Html.H1);
  // }
}
