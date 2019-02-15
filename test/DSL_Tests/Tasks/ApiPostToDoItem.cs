namespace U2U.AspNetCore.ScreenPlay
{

  using Core.Entities;
  using System.Threading.Tasks;
  using System;
  using Xunit;
  using DSL_Tests;
  using Newtonsoft.Json;
  using WebSite.ViewModels.ToDo;

  public class ApiPostToDoItem : ITask
  {
    
    private ToDoItem item;
    
    public ApiPostToDoItem(ToDoItem item) {
      this.item = item ?? throw new ArgumentNullException(nameof(item));
    }
    
    public async Task PerformAsAsync(Actor actor)
    {
      ApiClient client = actor.GetAbility<ApiClient>();
      Assert.NotNull(client);
      
      CreateViewModel vm = actor.Map<CreateViewModel>(this.item);
      string json = JsonConvert.SerializeObject(vm);
      
      await client.PostAsync(Uris.ApiToDos, json);
    }
  }


}
