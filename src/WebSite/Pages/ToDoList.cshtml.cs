using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webSite.Pages
{
  public class ToDoListModel : PageModel
  {
    private IToDoRepository repository;
    
    public ToDoListModel(IToDoRepository repository) {
      this.repository  = repository;
    }
    
    public IEnumerable<ToDoItem> ToDoItems { get; private set; }
    
    public void OnGet()
    {
      ToDoItems = this.repository.ToDos.ToList();
    }
  }
}
