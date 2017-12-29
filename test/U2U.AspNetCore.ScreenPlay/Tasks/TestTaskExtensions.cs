namespace U2U.AspNetCore.ScreenPlay
{
  public static class TestTaskExtensions
  {
    public static TestTask CouldGoToPage(this TestTask task, string uri)
    => task.AddAction(new Browses(uri));
    
    public static TestTask CouldGet(this TestTask task, string uri) 
    => task.AddAction(new GetResult(uri));
  }
}

