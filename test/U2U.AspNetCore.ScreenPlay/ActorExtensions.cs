namespace U2U.AspNetCore.ScreenPlay
{
  
  public static class ActorExtensions {
    
    public static Browser UsingBrowser(this Actor actor)
    => actor.GetAbility<Browser>();
    
    public static ApiClient UsingApiClient(this Actor actor)
    => actor.GetAbility<ApiClient>(); 
    
  }
}
