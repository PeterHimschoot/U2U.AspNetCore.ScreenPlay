namespace DSL_Tests
{
  using U2U.AspNetCore.ScreenPlay;

  public static class TestTaskExtensions
  {
    public static TestTask CouldGoToDefaultPage(this TestTask task)
      => task.AddAction(new Browses(Uris.HomePage));
    
    public static TestTask CouldGoToItemsPage(this TestTask task)
    => task.AddAction(new Browses(Uris.ItemsPage));
    
    public static TestTask HasToDoItems(this TestTask task, params string[] items) {
      foreach( var item in items) {
        task.AddAction( new AddInitialToDoItem(item));
      }
      return task;
    }
  }
  
  // public static class BrowserExtensions {
  //   public static Browser ShouldFindProperTitle(this Browser browser)
  //   => browser.ShouldFind(Html.H1);
  // }
}
