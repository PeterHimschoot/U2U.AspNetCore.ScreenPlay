using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers {
  public class ApiController : Controller {
    private IToDoRepository repository;
    
    public ApiController(IToDoRepository repository) {
     this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    [HttpGet("todo")]
    [Authorize(Policy = "CanListClaims")]
    public IQueryable<ToDoItem> GetToDos() 
    => this.repository.ToDos;
  }
}
