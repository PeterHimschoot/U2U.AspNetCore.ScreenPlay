namespace U2U.AspNetCore.ScreenPlay
{
  using System.Threading.Tasks;

  public interface ITask
  {
    Task PerformAsAsync(Actor actor);
  }
}
