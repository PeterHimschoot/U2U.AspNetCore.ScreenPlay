using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;

namespace U2U.AspNetCore.ScreenPlay
{
  public class ApiClient : HttpClient
  {
    public ApiClient(TestServer server) : base(server) { }

    public override string Name => "ApiClient";

    protected override async Task ProcessResponseAsync()
    {
      this.Result = await Response.Content.ReadAsStringAsync();
    }

    public string Result { get; private set; }

    public object JSON { get; set; }

    public ApiQuestions ShouldReturn() => new ApiQuestions(this);
  }
}
