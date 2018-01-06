namespace U2U.AspNetCore.ScreenPlay
{
  using System;

  public abstract class Question : IQuestion
  {
    public IHttpClient Assert(IHttpClient client)
    {
      var browser = client as Browser;
      if (browser != null)
      {
        return Assert(browser);
      }
      else
      {
        throw new ArgumentException(paramName: nameof(browser), message: "Questions require the HttpClient to be a Browser instance.");
      }
    }

    protected abstract Browser Assert(Browser browser);
  }
}
