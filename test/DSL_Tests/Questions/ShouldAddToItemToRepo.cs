namespace DSL_Tests
{
  using System;
  using System.Linq;
  using Core.Entities;
  using Core.Interfaces;
  using U2U.AspNetCore.ScreenPlay;
  using Xunit;

  public class ShouldAddToDoItemToRepo : IQuestion
  {
    private ToDoItem item;
    private IToDoRepository repository;

    public ShouldAddToDoItemToRepo(ToDoItem item, IToDoRepository repository)
    {
      this.item = item ?? throw new ArgumentNullException(nameof(item));
      this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    Browser IQuestion.Assert(Browser browser)
    {
      var items = repository.ToDos.ToList();
      Assert.Contains(items, (i) => i.Title == item.Title);
      return browser;
    }
  }
}
