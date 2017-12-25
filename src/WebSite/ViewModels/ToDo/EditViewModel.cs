namespace WebSite.ViewModels.ToDo
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using Core.Entities;

  public class EditViewModel
  {
    [Required]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public DateTime DeadLine { get; set; }
  }
}
