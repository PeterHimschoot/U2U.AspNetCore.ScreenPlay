namespace Core.Entities
{
  using System;

  public class ToDoItem : EntityBase
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public DateTime DeadLine { get; set; }
    public User User { get; set; }
  }
}
