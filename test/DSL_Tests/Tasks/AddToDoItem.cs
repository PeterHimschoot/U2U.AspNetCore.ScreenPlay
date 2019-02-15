
namespace DSL_Tests
{
  using System.Threading.Tasks;
  using Core.Entities;
  using Core.Interfaces;
  using U2U.AspNetCore.ScreenPlay;

  public class AddToDoItem : ITask
  {
    private ToDoItem item;
    
    public AddToDoItem(ToDoItem item)
    {
      this.item = item;
    }

    async Task ITask.PerformAsAsync(Actor actor)
    {
      IToDoRepository repo = actor.GetService<IToDoRepository>();
      repo.AddToDoItem(item);
      await repo.CommitAsync();
    }
  }
}
