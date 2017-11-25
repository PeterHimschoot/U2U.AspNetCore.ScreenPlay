using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace webSite
{
  public class ToDoController : Controller
  {
    private IToDoRepository repository;
    private ILogger<ToDoController> logger;

    public ToDoController(IToDoRepository repository, ILogger<ToDoController> logger)
    {
      this.repository = repository;
      this.logger = logger;
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
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateViewModel vm)
    {
      logger.LogInformation("Inside Create/POST method");
      if (!ModelState.IsValid)
      {
        return View();
      }
      this.repository.AddToDoItem(vm.Item);
      await this.repository.CommitAsync();
      return RedirectToPage("/Index");
    }
  }
}
