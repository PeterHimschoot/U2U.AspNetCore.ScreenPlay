using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace webSite.Pages
{
  public class ToDoController : Controller
  {
    private IToDoRepository repository;

    public ToDoController(IToDoRepository repository)
    {
      this.repository = repository;
    }

    [HttpGet("Create")]
    public ActionResult Create()
    {
      var item = new ToDoItem
      {
        Title = "TODO",
        Description = "Don't forget to...",
        Completed = false,
        DeadLine = DateTime.Now.AddDays(5)
      };
      var vm = new CreateViewModel { Item = item };
      return View(vm);
    }

    [HttpPost("Create")]
    public ActionResult Create(CreateViewModel vm)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }
      return new OkResult();

      // this.repository.AddToDoItem(this.Item);

      // await this.repository.CommitAsync();
      // return RedirectToPage("/Index");
    }
  }
}
