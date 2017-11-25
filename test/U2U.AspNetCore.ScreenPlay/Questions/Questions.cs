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
      question.Assert(this.browser);
      return this;
    }

    Browser IQuestion.Assert(Browser browser)
    {
      foreach (var question in questions)
      {
        question.Assert(browser);
      }
      return this.browser;
    }
  }
}
