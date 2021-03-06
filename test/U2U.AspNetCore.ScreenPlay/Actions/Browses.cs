
namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Threading.Tasks;

  public sealed class Browses : ITask
  {
    public string Uri { get; }

    public Browses(string uri)
    {
      this.Uri = uri;
    }

    public async Task PerformAsAsync(Actor actor)
    {
      Browser browser = actor.GetAbility<Browser>(); 
      if( browser != null ) {
        await browser.ToOpenPageAsync(this.Uri);
      } else {
        throw new Exception(message: $"The actor {actor.Name} does not have the browser ability.");
      }
    }
  }
}
