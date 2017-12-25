
namespace DSL_Tests
{
  using System.Threading.Tasks;
  using Core.Entities;
  using Core.Interfaces;
  using U2U.AspNetCore.ScreenPlay;

  public class AddInitialToDoItem : IAction
  {
    private string title;
    
    public AddInitialToDoItem(string title)
    {
      this.title = title;
    }

    async Task IAction.PerformAsAsync(Actor actor)
    {
      IToDoRepository repo = actor.GetAbility<IToDoRepository>();
      repo.AddToDoItem(new ToDoItem { Title = this.title });
      await repo.CommitAsync();
    }
  }
}
