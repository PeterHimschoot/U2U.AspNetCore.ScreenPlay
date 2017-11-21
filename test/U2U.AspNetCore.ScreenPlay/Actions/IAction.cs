namespace U2U.AspNetCore.ScreenPlay
{
  using System.Threading.Tasks;

  public interface IAction
  {
    Task PerformAsAsync(Actor actor);
  }
}
