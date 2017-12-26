
namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Threading.Tasks;

  public sealed class Browses : IAction
  {
    public string Uri { get; }

    public Browses(string uri)
    {
      this.Uri = uri;
    }

    public async Task PerformAsAsync(Actor actor)
    {
      if( actor.HasBrowser ) {
        await actor.UsesBrowser.ToOpenPageAsync(this.Uri);
      } else {
        throw new Exception(message: "This actor does not have a browser ability.");
      }
    }
  }
}
