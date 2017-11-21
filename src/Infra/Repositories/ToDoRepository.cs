namespace Infra.Repositories
{

  using System;
  using System.Linq;
  using System.Threading.Tasks;
  using Core.Entities;
  using Core.Interfaces;
  using Infra.DbContexts;
  using Microsoft.EntityFrameworkCore;

  public class ToDoRepository : IToDoRepository
  {
    private ToDoDb db;

    public ToDoRepository(ToDoDb db)
    {
      this.db = db;
    }

    public IQueryable<User> Users => db.Users.AsNoTracking();

    public IQueryable<ToDoItem> ToDos => db.ToDos.AsNoTracking();

    public void AddToDoItem(ToDoItem item)
    {
      this.db.ToDos.Add(item);
    }

    public void ChangeToDoItem(ToDoItem item)
    {
      this.db.ToDos.Attach(item);
      this.db.Entry<ToDoItem>(item).State = EntityState.Modified;
    }

    public void RemoveToDoItem(ToDoItem item)
    {
      this.db.ToDos.Attach(item);
      this.db.Entry<ToDoItem>(item).State = EntityState.Deleted;
    }

    public async Task CommitAsync()
    {
      await this.db.SaveChangesAsync();
    }
  }
}
