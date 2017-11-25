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

  public class FormValues
  {
    private readonly List<KeyValuePair<string, string>> variables = new List<KeyValuePair<string, string>>();

    public IEnumerable<KeyValuePair<string, string>> Values => variables;

    public FormValues Add(Expression<Func<object, string>> expression)
    {
      var kv = new FormPropertyBuilder().Infer(expression);
      return this.Add(kv);
    }

    public FormValues Add(KeyValuePair<string, string> kv)
    {
      this.variables.Add(kv);
      return this;
    }

    public FormValues Add(string key, string value)
    => Add(new KeyValuePair<string, string>(key, value));
  }
}
