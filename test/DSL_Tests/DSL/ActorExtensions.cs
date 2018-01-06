namespace DSL_Tests
{
  using AutoMapper;
  using U2U.AspNetCore.ScreenPlay;

  public static class ActorExtensions
  {
    public static T Map<T>(this Actor actor, object obj)
    {
      IMapper mapper = actor.GetService<IMapper>();
      return mapper.Map<T>(obj);
    }
  }
}
