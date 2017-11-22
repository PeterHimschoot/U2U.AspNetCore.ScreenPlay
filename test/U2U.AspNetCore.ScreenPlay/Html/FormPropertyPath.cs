namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Linq.Expressions;
  using System.Collections.Generic;

  public class FormPropertyBuilder
  {

    public KeyValuePair<string,string> Infer(Expression<Func<object, string>> expression)
    {
      string value = (expression.Compile())(null);
      MemberExpression memberExpression = (MemberExpression)expression.Body;
      string path = null;
      while (memberExpression.Expression is MemberExpression)
      {
        if (path == null)
        {
          path = memberExpression.Member.Name;
        }
        else
        {
          path = memberExpression.Member.Name + "." + path;
        }
        memberExpression = (MemberExpression)memberExpression.Expression;
      }
      return new KeyValuePair<string,string>(path, value);
    }
  }
}
