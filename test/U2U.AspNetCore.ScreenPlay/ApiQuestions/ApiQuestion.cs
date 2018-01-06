namespace U2U.AspNetCore.ScreenPlay
{
  using System;

  public abstract class ApiQuestion : IApiQuestion
  {
    public IHttpClient Assert(IHttpClient client)
    {
      var apiClient = client as ApiClient;
      if (apiClient != null)
      {
        return Assert(apiClient);
      }
      else
      {
        throw new ArgumentException(paramName: nameof(client), message: "An ApiQuestion requires the client the be an ApiClient");
      }
    }

    protected abstract ApiClient Assert(ApiClient client);
  }
}
