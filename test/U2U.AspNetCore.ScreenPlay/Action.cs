using System.Threading.Tasks;

namespace U2U.AspNetCore.ScreenPlay
{

  public abstract class Action
  {

    public abstract Task PerformAsAsync(Actor actor);


  }

  public class Browses : Action
  {
    public string Uri { get; }
    
    public Browses(string uri)
    {
      this.Uri = uri;
    }
    
    public override async Task PerformAsAsync(Actor actor)
    {
      await actor.UsesBrowser.ToOpenPageAsync(this.Uri);
    }

  }

}
