namespace WebSite.ViewModels.ToDo
{
  using System.Collections.Generic;
  using Core.Entities;
  
  public class ToDosViewModel
  {
    public IEnumerable<ToDoItem> ToDoItems { get; set; }
  }
}
