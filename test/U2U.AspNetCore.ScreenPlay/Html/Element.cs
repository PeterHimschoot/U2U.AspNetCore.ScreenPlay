namespace U2U.AspNetCore.ScreenPlay
{

  // using HtmlAgilityPack;
  using System.Linq;
  using System.Collections.Generic;
  using Xunit;
  using System;

  public class Element
  {
    // private HtmlNode node;
    // public Element(HtmlNode node)
    // {
    //   this.node = node;
    // }
    
    // public string InnerText => this.node.InnerText;

    // public Element WithContents(string text)
    // {
    //   Assert.Equal(this.node.InnerText, text);
    //   return this;
    // }

    // public Element WithContentsIn(IEnumerable<string> strings)
    // {
    //   Assert.Contains(strings, s => this.node.InnerText == s);
    //   return this;
    // }

    // public Element WithAChild(Action<Element> assertion)
    // {
    //   foreach (var n in this.node.ChildNodes)
    //   {
    //     assertion(new Element(n));
    //   }
    //   return this;
    // }
  }
  
  // public static partial class ScreenPlayExtenions
  // {
  //   private static IEnumerable<HtmlNode> LookFor(this HtmlNode root, string type, string @class)
  //   {
  //     return root.DescendantsAndSelf(type).Where(n => n.Attributes["class"].Value == @class);
  //   }

  //   public static HtmlNode DivWithClass(this HtmlNode root, string @class)
  //   {
  //     var nodes = root.LookFor(Html.Div, @class);
  //     return nodes.Single();
  //   }
  // }
}
