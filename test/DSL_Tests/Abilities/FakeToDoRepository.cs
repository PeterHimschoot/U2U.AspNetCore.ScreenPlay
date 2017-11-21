namespace DSL_Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Core.Entities;
  using Core.Interfaces;
  using U2U.AspNetCore.ScreenPlay;

  public class FakeToDoRepository : IAbility, IToDoRepository
  {
    string IAbility.Name {get;} = "FakeRepository";

    public FakeToDoRepository() 
    {
      this.Users = new List<User>().AsQueryable();
      this.ToDos = new List<ToDoItem>().AsQueryable();
    }
    
    public IQueryable<User> Users { get; set; }

    public IQueryable<ToDoItem> ToDos { get; set; }

    public void AddToDoItem(ToDoItem item)
    {
      var newItems = new List<ToDoItem>(this.ToDos);
      newItems.Add(item);
      this.ToDos = newItems.AsQueryable();
    }

    public void ChangeToDoItem(ToDoItem item)
    {
      throw new NotImplementedException();
    }

    public Task CommitAsync()
    {
      return Task.CompletedTask;
    }

    public void RemoveToDoItem(ToDoItem item)
    {
      var newItems = new List<ToDoItem>(this.ToDos);
      newItems.Remove(item);
      this.ToDos = newItems.AsQueryable();
    }
  }

}
