
namespace DSL_Tests
{
  using System.Threading.Tasks;
  using Core.Entities;
  using Core.Interfaces;
  using U2U.AspNetCore.ScreenPlay;

  public class AddToDoItem : IAction
  {
    private ToDoItem item;
    
    public AddToDoItem(ToDoItem item)
    {
      this.item = item;
    }

    async Task IAction.PerformAsAsync(Actor actor)
    {
      IToDoRepository repo = actor.GetAbility<IToDoRepository>();
      repo.AddToDoItem(item);
      await repo.CommitAsync();
    }
  }
}
