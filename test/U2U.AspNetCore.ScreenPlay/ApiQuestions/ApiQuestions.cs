namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;

  public class ApiQuestions : IApiQuestion
  {
    private ApiClient client;

    public ApiQuestions(ApiClient client)
    {
      this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    private List<IApiQuestion> questions = new List<IApiQuestion>();

    public ApiQuestions Add(IApiQuestion question)
    {
      question.Assert(this.client);
      return this;
    }

    ApiClient IApiQuestion.Assert(ApiClient client)
    {
      foreach (var question in questions)
      {
        question.Assert(client);
      }
      return this.client;
    }
  }
}
