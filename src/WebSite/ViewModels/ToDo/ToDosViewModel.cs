using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace webSite
{
  public class ToDosViewModel
  {
    public IEnumerable<ToDoItem> ToDoItems { get; set; }
  }
}
