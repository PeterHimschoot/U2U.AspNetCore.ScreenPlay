namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;

  public class Questions : IQuestion
  {
    private DOM dom;

    public Questions(DOM dom)
    {
      this.dom = dom ?? throw new ArgumentNullException(nameof(dom));
    }

    private List<IQuestion> questions = new List<IQuestion>();

    public Questions Add(IQuestion question)
    {
      question.Assert(this.dom);
      // questions.Add(question);
      return this;
    }

    DOM IQuestion.Assert(DOM dom)
    {
      foreach (var question in questions)
      {
        question.Assert(dom);
      }
      return dom;
    }
  }
}
