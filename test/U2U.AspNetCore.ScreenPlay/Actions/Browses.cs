
namespace U2U.AspNetCore.ScreenPlay
{
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
      await actor.UsesBrowser.ToOpenPageAsync(this.Uri);
    }
  }
}
