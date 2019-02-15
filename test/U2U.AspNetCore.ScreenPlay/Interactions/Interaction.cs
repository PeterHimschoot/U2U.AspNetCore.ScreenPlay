using System.Collections.Generic;
using System.Threading.Tasks;

namespace U2U.AspNetCore.ScreenPlay
{
  public class Interaction
  {
    public Interaction(Actor actor) {
      this.Actor = actor;
    }
    
    public Actor Actor {get;}
    
    private List<ITask> Tasks { get; } = new List<ITask>();
    
    public Interaction Add(ITask action) {
      this.Tasks.Add(action);
      return this;
    }
    
    public async Task PerformAsync() {
      foreach( var action in this.Tasks ) {
        await action.PerformAsAsync(this.Actor);
      }
    }
    
    public async Task Successfully() {
      await this.PerformAsync();
    }
    
    public Interaction And() => this;
  }
}
