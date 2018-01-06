namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using System.Net.Http.Headers;

  public class ShouldHaveHeader : Question
  {
    public ShouldHaveHeader(string key, Func<IEnumerable<string>, bool> predicate)
    {
      this.HeaderKey = key;
      this.HeaderPredicate = predicate;
    }

    public ShouldHaveHeader(string key, string withValue)
    : this(key, values => values.Any(value => value == withValue)) { }

    public string HeaderKey { get; set; }
    public Func<IEnumerable<string>, bool> HeaderPredicate { get; set; }

    protected override Browser Assert(Browser browser)
    {
      HttpResponseHeaders headers = browser.Response.Headers;
      var hasHeader = headers.Any(header => header.Key == HeaderKey && HeaderPredicate(header.Value));
      Xunit.Assert.True(hasHeader, userMessage: $"Response does not have correct {HeaderKey} header");
      return browser;
    }
  }
}
