using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSite.ViewModels.ToDo;

namespace WebSite.Controllers
{
  public class ApiController : Controller
  {
    public class Routes
    {
      public const string CreatedAtRoute = "";
    }

    private IToDoRepository repository;
    private IMapper mapper;

    public ApiController(IToDoRepository repository, IMapper mapper)
    {
      this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
      this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("todo")]
    [Authorize(Policy = "CanListItems")]
    public IQueryable<ToDoItem> GetToDos()
    => this.repository.ToDos;

    [HttpGet()]
    [Route("todo/{id:int:min(0)}", Name = nameof(Routes.CreatedAtRoute))]
    [Authorize(Policy = "CanListItems")]
    public async Task<IActionResult> GetToDo(int id)
    {
      var item = await this.repository.ToDos.SingleOrDefaultAsync(todo => todo.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      return Ok(item);
    }

    [HttpPost("todo")]
    [Authorize(Policy = "CanModifyItems")]
    public async Task<IActionResult> PostToDo(CreateViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      var toDoItem = mapper.Map<ToDoItem>(viewModel);
      this.repository.AddToDoItem(toDoItem);
      await this.repository.CommitAsync();
      return CreatedAtRoute(routeName: nameof(Routes.CreatedAtRoute), value: new { id = toDoItem.Id });
    }
  }
}
