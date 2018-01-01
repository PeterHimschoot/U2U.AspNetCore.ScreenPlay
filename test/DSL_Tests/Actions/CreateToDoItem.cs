using System.Threading.Tasks;
using U2U.AspNetCore.ScreenPlay;
using WebSite.ViewModels.ToDo;

namespace DSL_Tests {

  public class CreateToDoItem : IAction
  {
    private CreateViewModel model;
    public CreateToDoItem(CreateViewModel model) {
     this.model = model; 
    }
    public async Task PerformAsAsync(Actor actor)
    {
      var browser = actor.Browser();
      var formValues = new FormValues()
      .Add(x => model.Title)
      .Add(x => model.Description);
      // .Add(x => model.DeadLine) // TODO
      await browser.PostToControllerAsync(Uris.Create, formValues);
    }
  }
}
