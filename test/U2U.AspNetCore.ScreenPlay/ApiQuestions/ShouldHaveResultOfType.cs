namespace U2U.AspNetCore.ScreenPlay
{
  using Newtonsoft.Json;
  
  public class ShouldHaveResultOfType<T> : ApiQuestion
  {
    protected override ApiClient Assert(ApiClient client)
    {
      client.JSON = JsonConvert.DeserializeObject<T>(client.Result);
      return client;
    }
  }
}
