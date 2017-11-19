using System;
using System.Collections.Generic;

namespace U2U.AspNetCore.ScreenPlay {
  
  public abstract class Question {
    
    public abstract DOM Assert(DOM dom);
  }
  
  public class Questions : Question {
    
    private DOM dom;
    
    public Questions(DOM dom) {
      this.dom = dom ?? throw new ArgumentNullException(nameof(dom));
    }
    
    private List<Question> questions = new List<Question>();
    
    public Questions Add(Question question) {
      questions.Add(question);
      return this;
    }
    
    public override DOM Assert(DOM dom) {
      foreach( var question in questions ) {
        question.Assert(dom);
      }
      return dom;
    }
  }
}
