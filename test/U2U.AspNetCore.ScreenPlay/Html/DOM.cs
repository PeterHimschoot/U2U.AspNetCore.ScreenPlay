namespace U2U.AspNetCore.ScreenPlay
{
  using System.Linq;
  using System.Collections.Generic;
  using Xunit;
  using AngleSharp.Dom.Html;

  public class DOM
  {

    private IHtmlDocument document;

    public DOM(IHtmlDocument document)
    {
      this.document = document;
    }

    // private IEnumerable<HtmlNode> FindHtmlNode(string type, string @class = null, string id = null)
    // {
    //   var nodes = this.document["type"];
      
    //   var nodes = this.document.DocumentNode.Descendants(type);
    //   if (!string.IsNullOrEmpty(@class))
    //   {
    //     nodes = nodes.Where(n => n.HasClass(@class));
    //   }
    //   if (!string.IsNullOrEmpty(id))
    //   {
    //     nodes = nodes.Where(n => n.Id == id);
    //   }
    //   return nodes;
    // }

    // public IEnumerable<Element> Contain(string type, string @class = null, string id = null)
    // {
    //   return FindHtmlNode(type, @class, id).Select(n => new Element(n));
    // }

    // public Element ContainSingle(string type, string @class = null, string id = null)
    // {
    //   var nodes = FindHtmlNode(type, @class, id);
    //   Assert.Single(nodes);
    //   return nodes.Single().AsElement();
    // }
    
    public DOM Contain(string query, string[] items) {
      var elements = this.document.QuerySelectorAll(query);
      Assert.NotEmpty(elements);
      var mapped = elements.Select(el => el.InnerHtml).ToList();
      Assert.Equal(items, mapped);
      return this;
    }

    // public Element Have(string type, string @class = null, string id = null)
    // {
    //   var nodes = FindHtmlNode(type, @class, id);
    // }

    // private static IEnumerable<HtmlNode> LookFor(this HtmlNode root, string type, string @class)
    // {
    //   return root.DescendantsAndSelf(type).Where(n => n.Attributes["class"].Value == @class);
    // }

    // public static HtmlNode DivWithClass(this HtmlNode root, string @class)
    // {
    //   var nodes = root.LookFor(Html.Div, @class);
    //   return nodes.Single();
    // }
    
    public DOM Should() => this;
  }
}
