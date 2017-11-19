namespace U2U.AspNetCore.ScreenPlay
{

  using HtmlAgilityPack;
  using System.Linq;
  using System.Collections.Generic;
  using Xunit;

  public class Element
  {
    private HtmlNode node;
    public Element(HtmlNode node)
    {
      this.node = node;
    }
    
    public void WithContents(string text) {
      Assert.Equal(this.node.InnerText, text);
    }
  }
  public static partial class ScreenPlayExtenions
  {

    private static IEnumerable<HtmlNode> LookFor(this HtmlNode root, string type, string @class)
    {
      return root.DescendantsAndSelf(type).Where(n => n.Attributes["class"].Value == @class);
    }

    public static HtmlNode DivWithClass(this HtmlNode root, string @class)
    {
      var nodes = root.LookFor(Html.Div, @class);
      return nodes.Single();
    }
  }
}
