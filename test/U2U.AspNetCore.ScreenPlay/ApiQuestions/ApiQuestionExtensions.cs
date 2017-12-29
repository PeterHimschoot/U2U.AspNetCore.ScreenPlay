
namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http.Headers;

  public static class ApiQuestionExtensions
  {
    public static ApiQuestions And(this ApiQuestions questions)
    => questions;

    public static ApiQuestions Should(this ApiQuestions questions)
    => questions;

    public static ApiQuestions HaveStatusCode(this ApiQuestions questions, HttpStatusCode code)
    => questions.Add(new ShouldHaveApiStatusCode(code));

    public static ApiQuestions HaveContentType(this ApiQuestions questions, MediaTypeHeaderValue contentType)
    => questions.Add(new ShouldHaveApiContentType(contentType));

    public static ApiQuestions HaveResultOfType<T>(this ApiQuestions questions)
    => questions.Add(new ShouldHaveResultOfType<T>()); 
    
    public static ApiQuestions HaveResult<T>(this ApiQuestions questions, T resembling, IEqualityComparer<T> comparer)
    => questions.Add(new ShouldHaveResultOfType<T>())
                .Add(new ShouldHaveResult<T>(resembling, comparer));
    
    // public static ApiQuestions HaveContentType(this ApiQuestions q, string contentType)
    // => q.Add(new ShouldHaveContentType(contentType));

    // public static ApiQuestions HaveHeader(this ApiQuestions questions, string headerKey, Func<IEnumerable<string>, bool> predicate)
    //   => questions.Add(new ShouldHaveHeader(headerKey, predicate));

    // public static ApiQuestions HaveHeader(this ApiQuestions questions, string headerKey, string withValue)
    //   => questions.Add(new ShouldHaveHeader(headerKey, withValue));

    // public static ApiQuestions HaveContentsWith(this ApiQuestions questions, string path, params string[] items)
    // => questions.Add(new ShouldHaveContentsWith(path, items));
  }
}
