namespace U2U.AspNetCore.ScreenPlay.Tests
{
  using System;
  using Xunit;
  using U2U.AspNetCore.ScreenPlay;
  public class UnitTest1
  {
    class DeepHelper
    {
      public string X { get; set; }
    }
    class Helper
    {
      public string Name { get; set; }
      public DeepHelper Nested { get; set; }
    }
    
    [Fact]
    public void Test1()
    {
      var helper = new Helper
      {
        Name = "Naem",
        Nested = new DeepHelper
        {
          X = "Nested"
        }
      };
      
      var bob = new FormPropertyBuilder();
      var path = bob.Infer(x => helper.Name);
      Assert.Equal(nameof(helper.Name), path.Key);
      Assert.Equal("Naem", path.Value);

      path = bob.Infer(x => helper.Nested.X);
      Assert.Equal($"{nameof(helper.Nested)}.{nameof(helper.Nested.X)}", path.Key);
      Assert.Equal("Nested", path.Value);
    }
  }
}
