
namespace U2U.AspNetCore.ScreenPlay
{
  using System.Net;
  using System.Net.Http.Headers;

  public static class QuestionExtensions
  {
    public static Questions And(this Questions questions)
    => questions;

    public static Questions HaveStatusCode(this Questions q, HttpStatusCode code)
    => q.Add(new ShouldHaveStatusCode(code));

    public static Questions HaveContentType(this Questions q, MediaTypeHeaderValue contentType)
    => q.Add(new ShouldHaveContentType(contentType));
    
    public static Questions HaveContentType(this Questions q, string contentType)
    => q.Add(new ShouldHaveContentType(contentType));
  }
}
