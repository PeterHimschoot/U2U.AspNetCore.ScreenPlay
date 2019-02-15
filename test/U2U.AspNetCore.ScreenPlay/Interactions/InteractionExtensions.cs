namespace U2U.AspNetCore.ScreenPlay
{
  public static class InteractionExtensions
  {
    public static Interaction CouldGoToPage(this Interaction task, string uri)
    => task.Add(new Browses(uri));
    
    public static Interaction CouldGet(this Interaction task, string uri) 
    => task.Add(new GetResult(uri));
  }
}

