using System;
using System.Collections.Generic;

namespace U2U.AspNetCore.ScreenPlay {
  public static class EnumerableExtensions {
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> coll, Action<T> action) {
      foreach(var el in coll) { action(el); }
      return coll;
    }
  }
}
