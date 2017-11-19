namespace DSL_Tests
{

  using U2U.AspNetCore.ScreenPlay;

  public static class TestTaskExtensions
  {

    public static TestTask CouldGoToDefaultPage(this TestTask task)
    {

      task.AddAction(new Browses(Uris.HomePage));

      return task;
    }
  }
  
  // public static class BrowserExtensions {
  //   public static Browser ShouldFindProperTitle(this Browser browser)
  //   => browser.ShouldFind(Html.H1);
  // }
}
