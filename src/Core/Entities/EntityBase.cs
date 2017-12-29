namespace Core.Entities
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  
  public class EntityBase
  {

    [Key]
    public int Id { get; set; }
    
    public static readonly IEqualityComparer<EntityBase> DefaultComparer = new EntityBaseEqualityComparer();

    private class EntityBaseEqualityComparer : IEqualityComparer<EntityBase>
    {
      public bool Equals(EntityBase x, EntityBase y)
      => x.Id == y.Id;

      public int GetHashCode(EntityBase obj)
      => obj.Id.GetHashCode();
    }
  }
}
