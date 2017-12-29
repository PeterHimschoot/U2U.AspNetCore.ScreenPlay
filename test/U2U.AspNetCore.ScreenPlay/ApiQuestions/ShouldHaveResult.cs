namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;
  using Xunit;

  public class ShouldHaveResult<T> : IApiQuestion
  {
    private T expected;
    private IEqualityComparer<T> comparer;

    public ShouldHaveResult(T resembling, IEqualityComparer<T> comparer)
    {
      this.expected = resembling;
      this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    public ApiClient Assert(ApiClient client)
    {
      Type actualType = client.JSON.GetType();
      Type expectedType  = typeof(T);
      Xunit.Assert.True(actualType == expectedType, $"Result does not have expected type {expectedType.FullName}");
      T actual = (T)client.JSON;
      bool match = comparer.Equals(this.expected, expected);
      Xunit.Assert.True(match, $"Result does not match for {nameof(T)}");
      return client;
    }
  }
}
