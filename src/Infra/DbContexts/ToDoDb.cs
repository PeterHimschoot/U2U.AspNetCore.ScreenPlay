using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.DbContexts
{
  public class ToDoDb : DbContext
  {
    public ToDoDb(DbContextOptions<ToDoDb> options)
    : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<ToDoItem> ToDos { get; set; }
    
  }
}
