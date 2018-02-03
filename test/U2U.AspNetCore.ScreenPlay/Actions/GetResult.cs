
namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Threading.Tasks;

  public sealed class GetResult : IAction
  {
    public string Uri { get; }

    public GetResult(string uri)
    {
      this.Uri = uri;
    }

    public async Task PerformAsAsync(Actor actor)
    {
      var apiClient = actor.GetAbility<ApiClient>();
      if( apiClient != null ) {
        await apiClient.GetAsync(this.Uri);
      } else {
        throw new Exception(message: $"The actor {actor.Name} does not have the apiClient ability.");
      }
    }
  }
}
