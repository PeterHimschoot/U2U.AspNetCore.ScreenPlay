using System.Collections.Generic;
using System.Threading.Tasks;

namespace U2U.AspNetCore.ScreenPlay
{

  public class TestTask
  {
    public TestTask(Actor actor) {
      this.Actor = actor;
    }
    
    public Actor Actor {get;}
    
    private List<Action> Actions { get; } = new List<Action>();
    
    public TestTask AddAction(Action action) {
      this.Actions.Add(action);
      return this;
    }
    
    public async Task PerformAsync() {
      foreach( var action in this.Actions ) {
        await action.PerformAsAsync(this.Actor);
      }
    }
    
    public async Task Successfully() {
      await this.PerformAsync();
    }
    
    
    
  }


}
