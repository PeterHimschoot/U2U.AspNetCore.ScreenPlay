namespace WebSite
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc.RazorPages;
  using Core.Entities;
  using Microsoft.AspNetCore.Mvc;
  using Core.Interfaces;
  using Microsoft.Extensions.Logging;
  using WebSite.ViewModels.ToDo;
  using Microsoft.EntityFrameworkCore;
  using global::AutoMapper;

  public class ToDoController : Controller
  {
    private IToDoRepository repository;
    private ILogger<ToDoController> logger;
    private IMapper mapper;

    public ToDoController(IToDoRepository repository, IMapper mapper, ILogger<ToDoController> logger)
    {
      this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      this.logger = logger;
    }

    [HttpGet("todos")]
    [HttpGet("")]
    public ActionResult ToDos()
    {
      var vm = new ToDosViewModel
      {
        ToDoItems = repository.ToDos.ToList()
      };
      return View(vm);
    }

    [HttpGet("Create")]
    public ActionResult Create()
    {
      var vm = new CreateViewModel
      {
        Title = "TODO",
        Description = "Don't forget to...",
        DeadLine = DateTime.Now.AddDays(5)
      };
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
      var toDoItem = mapper.Map<ToDoItem>(vm);
      this.repository.AddToDoItem(toDoItem);
      await this.repository.CommitAsync();
      return RedirectToAction(nameof(ToDoController.ToDos));
    }
    
    [HttpGet("Edit/{id:int:min(0)}")]
    public async Task<ActionResult> Edit(int id) {
      var toDoItem = await this.repository.ToDos.SingleAsync(todo => todo.Id == id);
      var vm = mapper.Map<EditViewModel>(toDoItem);
      return View(vm);
    }
    
    [HttpPost("Edit/{id:int:min(0)}")]
    public async Task<ActionResult> Edit(int id, EditViewModel viewModel) {
      if( !ModelState.IsValid) {
        return View();
      }
      var toDoItem = mapper.Map<ToDoItem>(viewModel);
      this.repository.ChangeToDoItem(toDoItem);
      await this.repository.CommitAsync();
      return RedirectToAction(nameof(ToDoController.ToDos));
    }
  }
}
