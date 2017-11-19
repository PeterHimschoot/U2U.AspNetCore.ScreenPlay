namespace U2U.AspNetCore.ScreenPlay
{

  using HtmlAgilityPack;
  using System.Linq;
  using System.Collections.Generic;

  public class DOM
  {

    private HtmlDocument document;

    public DOM(HtmlDocument document)
    {
      this.document = document;
    }

    private IEnumerable<HtmlNode> ContainHtmlNode(string type, string @class)
    {
      var nodes = this.document.DocumentNode.DescendantsAndSelf(type);
      if (!string.IsNullOrEmpty(@class))
      {
        nodes = nodes.Where(n => n.HasClass(@class));
      }
      return nodes;
    }

    public IEnumerable<Element> Contain(string type, string @class = null)
    {
      var nodes = this.document.DocumentNode.DescendantsAndSelf(type);
      if (!string.IsNullOrEmpty(@class))
      {
        nodes = nodes.Where(n => n.HasClass(@class));
      }
      return ContainHtmlNode(type, @class).Select(n => new Element(n));
    }

    public Element ContainSingle(string type, string @class = null)
    {
      return ContainHtmlNode(type, @class).Single().AsElement();
    }

    // private static IEnumerable<HtmlNode> LookFor(this HtmlNode root, string type, string @class)
    // {
    //   return root.DescendantsAndSelf(type).Where(n => n.Attributes["class"].Value == @class);
    // }

    // public static HtmlNode DivWithClass(this HtmlNode root, string @class)
    // {
    //   var nodes = root.LookFor(Html.Div, @class);
    //   return nodes.Single();
    // }
  }
}
