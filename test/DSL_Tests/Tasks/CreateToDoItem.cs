using System.Threading.Tasks;
using U2U.AspNetCore.ScreenPlay;
using WebSite.ViewModels.ToDo;

namespace DSL_Tests
{

  public class CreateToDoItem : ITask
  {
    private CreateViewModel model;
    public CreateToDoItem(CreateViewModel model)
      => this.model = model;
    public async Task PerformAsAsync(Actor actor)
    {
      Browser browser = actor.UsingBrowser(); // get ability
      FormValues formValues = new FormValues()
      .Add(x => model.Title)
      .Add(x => model.Description);
      await browser.PostToControllerAsync(Uris.Create, formValues);
    }
  }
}
