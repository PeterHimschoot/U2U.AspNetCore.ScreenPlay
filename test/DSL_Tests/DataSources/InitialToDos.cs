using Core.Entities;

namespace DSL_Tests
{
  public static partial class TestData
  {
    public static readonly ToDoItem[] InitialToDos = new ToDoItem[] {
      new ToDoItem { Id = 1, Title = "Make coffee" },
      new ToDoItem { Id = 2, Title = "Feed the cat" },
    };
    
    public static readonly ToDoItem AddedItem 
    = new ToDoItem { Id = 1, Title = ""};
  }
}
