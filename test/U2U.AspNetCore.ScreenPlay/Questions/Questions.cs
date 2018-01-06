namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;

  public class Questions : IQuestion
  {
    private Browser browser;

    public Questions(Browser browser)
    {
      this.browser = browser ?? throw new ArgumentNullException(nameof(browser));
    }

    private List<IQuestion> questions = new List<IQuestion>();

    public Questions Add(IQuestion question)
    {
      questions.Add(question);
      return this;
    }

    IHttpClient IQuestion.Assert(IHttpClient client)
    {
      foreach (var question in questions)
      {
        question.Assert(client);
      }
      return client;
    }
  }
}
