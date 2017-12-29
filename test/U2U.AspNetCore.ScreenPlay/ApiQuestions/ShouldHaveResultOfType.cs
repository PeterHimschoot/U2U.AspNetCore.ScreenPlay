namespace U2U.AspNetCore.ScreenPlay
{
  using Newtonsoft.Json;
  
  public class ShouldHaveResultOfType<T> : IApiQuestion
  {
    ApiClient IApiQuestion.Assert(ApiClient client)
    {
      client.JSON = JsonConvert.DeserializeObject<T>(client.Result);
      return client;
    }
  }
}
