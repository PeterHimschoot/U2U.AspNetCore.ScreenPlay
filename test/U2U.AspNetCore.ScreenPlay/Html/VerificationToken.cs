namespace U2U.AspNetCore.ScreenPlay
{
  using System.Linq;
  using System.Net.Http;
  using AngleSharp.Dom.Html;

  public class VerificationToken
  {
    public const string Name = "__RequestVerificationToken";

    private VerificationToken(string token) => this.Token = token;

    public string Token {get;}
    
    public static VerificationToken From(IHtmlDocument document)
    {
      var verificationToken = string.Empty;
      var hiddenInputs = document.QuerySelectorAll("input").AsEnumerable();
      foreach (IHtmlInputElement input in hiddenInputs)
      {
        var name = input.Name;
        var value = input.Value;
        if (name == VerificationToken.Name)
        {
          verificationToken = value;
          return new VerificationToken(verificationToken);
        }
      }
      return null;
    }
  }
}
