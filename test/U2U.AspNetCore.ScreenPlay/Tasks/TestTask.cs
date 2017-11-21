using System.Collections.Generic;
using System.Threading.Tasks;

namespace U2U.AspNetCore.ScreenPlay
{
  // Called this TestTask to avoid conflict with System.Threading.Tasks.Task
  public class TestTask
  {
    public TestTask(Actor actor) {
      this.Actor = actor;
    }
    
    public Actor Actor {get;}
    
    // A task is a collection of actions which get performed
    private List<IAction> Actions { get; } = new List<IAction>();
    
    public TestTask AddAction(IAction action) {
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
    
    public TestTask And() => this;
    
    
  }


}
