namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http.Headers;

  public static class QuestionExtensions
  {
    public static Questions And(this Questions questions)
    => questions;

    public static Questions Should(this Questions questions)
    => questions;

    public static Questions HaveStatusCode(this Questions q, HttpStatusCode code, Uri location = null)
    => q.Add(new ShouldHaveStatusCode(code, location));
    
    public static Questions HaveStatusCode(this Questions q, HttpStatusCode code, string location)
    => q.HaveStatusCode(code, new Uri(location, UriKind.Relative));
    
    public static Questions HaveStatusCode(this Questions q, HttpStatusCode code)
    => q.HaveStatusCode(code, (Uri) null);

    public static Questions HaveContentType(this Questions q, MediaTypeHeaderValue contentType)
    => q.Add(new ShouldHaveContentType(contentType));

    public static Questions HaveContentType(this Questions q, string contentType)
    => q.Add(new ShouldHaveContentType(contentType));

    public static Questions HaveHeader(this Questions questions, string headerKey, Func<IEnumerable<string>, bool> predicate)
      => questions.Add(new ShouldHaveHeader(headerKey, predicate));

    public static Questions HaveHeader(this Questions questions, string headerKey, string withValue)
      => questions.Add(new ShouldHaveHeader(headerKey, withValue));

    public static Questions HaveContentsWith(this Questions questions, string path, params string[] items)
    => questions.Add(new ShouldHaveContentsWith(path, items));
  }
}
