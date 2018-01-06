namespace U2U.AspNetCore.ScreenPlay
{
  
  public static class ActorExtensions {
    
    public static Browser Browser(this Actor actor)
    => actor.GetAbility<Browser>();
    
    public static ApiClient ApiClient(this Actor actor)
    => actor.GetAbility<ApiClient>(); 
    
  }
}
