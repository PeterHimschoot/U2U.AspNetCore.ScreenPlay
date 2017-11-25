using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;
using U2U.AspNetCore.ScreenPlay;

namespace DSL_Tests
{

  // public class AddAToDoItemAction : IAction
  // {
  //   private string name;

  //   private AddAToDoItemAction(string name)
  //   {
  //     this.name = name;
  //   }

  //   public static AddAToDoItemAction Called(string name)
  //   {
  //     return new AddAToDoItemAction(name);
  //   }

  //   async Task IAction.PerformAsAsync(Actor actor)
  //   {
  //     var requestBuilder = actor.UsesBrowser.CreateRequest("/Create");

  //     requestBuilder.And(msg =>
  //     {
  //       msg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        
  //     });

  //     var response = await requestBuilder.PostAsync();

  //     await actor.Browser().SetResponse(response);
  //   }
  // }
}
