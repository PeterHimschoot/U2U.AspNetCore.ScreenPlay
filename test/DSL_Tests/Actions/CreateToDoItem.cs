using System.Threading.Tasks;
using U2U.AspNetCore.ScreenPlay;
using webSite;

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
      .Add(x => model.Item.Title)
      .Add(x => model.Item.Description);
      await browser.PostToControllerAsync(Uris.Create, formValues);
    }
  }
}
