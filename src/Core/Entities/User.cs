using System;
using System.Collections.Generic;

namespace Core.Entities {
  
  public class User : EntityBase {
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Email {get; set;}
    public List<ToDoItem> ToDos {get;set;}
    
    public void AddToDo(ToDoItem toDo) {
      if( toDo.DeadLine < DateTime.Now ) {
        throw new ArgumentException(message: $"This item is set for the past", paramName: nameof(toDo));
      }
      toDo.User = this;
      this.ToDos.Add(toDo);
    }
  }
}
