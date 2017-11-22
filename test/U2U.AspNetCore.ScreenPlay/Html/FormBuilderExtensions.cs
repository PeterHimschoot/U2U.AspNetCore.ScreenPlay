namespace U2U.AspNetCore.ScreenPlay {
  
  using System;
  using System.Linq.Expressions;
  
  public static class FormBuilderExtensions {
    
    // public static FormBuilder AddForm(this FormBuilder builder, Expression<Func<object,string>> expression) {
      
    //   var kv = new FormPropertyBuilder().Infer(expression);
    //   var value = (expression.Compile()).Invoke(default(Object));
    //   builder.AddVariable(name, value);
    //   return builder;
    // }
    
    // public static FormBuilder AddForm<O>(this FormBuilder builder, Expression<Func<O, DateTime>> expression) {
    //   var name = (expression.Body as MemberExpression).Member.Name;
    //   var value = (expression.Compile()).Invoke(default(O));
    //   builder.AddVariable(name,value.ToString("dd/MM/yyyy"));
    //   return builder;
    // }
    
    // public static FormBuilder AddForm<O>(this FormBuilder builder, Expression<Func<O, Boolean>> expression) {
    //   var name = (expression.Body as MemberExpression).Member.Name;
    //   var value = (expression.Compile()).Invoke(default(O));
    //   builder.AddVariable(name, value.ToString());
    //   return builder;
    // }
  }
}
