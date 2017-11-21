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
  public class CreateModel : PageModel
  {
    private IToDoRepository repository;

    public CreateModel(IToDoRepository repository)
    {
      this.repository = repository;
    }

    [BindProperty]
    public ToDoItem Item { get; set; }

    public void OnGet()
    {
      this.Item = new ToDoItem
      {
        Title = "TODO",
        Description = "Don't forget to...",
        Completed = false,
        DeadLine = DateTime.Now.AddDays(5)
      };
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      this.repository.AddToDoItem(this.Item);
      
      await this.repository.CommitAsync();
      return RedirectToPage("/Index");
    }
  }
}
