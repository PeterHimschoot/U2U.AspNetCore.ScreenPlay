namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Net;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Threading.Tasks;
  using AngleSharp.Dom.Html;
  using AngleSharp.Parser.Html;

  public class FormBuilder
  {
    public FormBuilder() { }

    private readonly List<KeyValuePair<string, string>> variables = new List<KeyValuePair<string, string>>();

    public FormBuilder Add(Expression<Func<object, string>> expression)
    {
      var kv = new FormPropertyBuilder().Infer(expression);
      return this.Add(kv);
    }

    public FormBuilder Add(KeyValuePair<string, string> kv)
    {
      this.variables.Add(kv);
      return this;
    }

    public FormBuilder Add(string key, string value)
    => Add(new KeyValuePair<string, string>(key, value));

    private async Task<string> GetVerificationTokenFromInput(HttpResponseMessage responseMessage)
    {
      var parser = new HtmlParser();
      var document = await parser.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());

      var verificationToken = string.Empty;
      var hiddenInputs = document.QuerySelectorAll("input").AsEnumerable();
      foreach (IHtmlInputElement input in hiddenInputs)
      {
        var name = input.Name;
        var value = input.Value;
        if (name == "__RequestVerificationToken")
        {
          verificationToken = value;
        }
      }
      return verificationToken;
    }

    private string verificationToken;
    private string verificationCookie;

    public async Task<FormBuilder> CopyCSRFToken(HttpResponseMessage responseMessage)
    {
      verificationToken = await GetVerificationTokenFromInput(responseMessage);
      verificationCookie = responseMessage.Headers.GetValues("Set-Cookie")
                               .FirstOrDefault(x => x.Contains(".Antiforgery"));

      this.Add("__RequestVerificationToken", verificationToken);
      return this;
    }

    public FormUrlEncodedContent Create()
    {
      var form = new FormUrlEncodedContent(variables);
      form.Headers.Add("Set-Cookie", verificationCookie);
      form.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
      return form;
    }
  }
}
