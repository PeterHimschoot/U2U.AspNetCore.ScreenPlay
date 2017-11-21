namespace Core.Entities
{

  using System.ComponentModel.DataAnnotations;
  
  public class EntityBase
  {

    [Key]
    public int Id { get; set; }
  }
}
