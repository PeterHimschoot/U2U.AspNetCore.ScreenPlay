using System;
using System.Linq;
using Core.Entities;
using Core.Interfaces;
using U2U.AspNetCore.ScreenPlay;

namespace DSL_Tests
{
  public class ShouldAddToDoItemToRepo : Question
  {
    private ToDoItem item;
    private IToDoRepository repository;

    public ShouldAddToDoItemToRepo(ToDoItem item, IToDoRepository repository)
    {
      this.item = item ?? throw new ArgumentNullException(nameof(item));
      this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected override Browser Assert(Browser browser)
    {
      var items = repository.ToDos.ToList();
      Xunit.Assert.Contains(items, (i) => i.Title == item.Title);
      return browser;
    }
  }
}
