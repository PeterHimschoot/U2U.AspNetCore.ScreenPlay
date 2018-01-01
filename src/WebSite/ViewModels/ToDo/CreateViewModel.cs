namespace WebSite.ViewModels.ToDo
{
  using System;
  using Core.Entities;

  public class CreateViewModel
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine { get; set; }
  }
}
