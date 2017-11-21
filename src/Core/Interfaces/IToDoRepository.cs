using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces {
  
  public interface IToDoRepository {
    
    IQueryable<User> Users {get;}
    
    IQueryable<ToDoItem> ToDos {get;}
    
    void AddToDoItem(ToDoItem item);
    
    void ChangeToDoItem(ToDoItem item);
    
    void RemoveToDoItem(ToDoItem item);
    
    Task CommitAsync();
    
  }
}
