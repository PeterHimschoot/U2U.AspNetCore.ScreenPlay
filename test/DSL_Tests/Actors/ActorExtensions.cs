using AutoMapper;
using Core.Interfaces;
using U2U.AspNetCore.ScreenPlay;

namespace DSL_Tests
{
  public static class ActorExtensions
  {
    public static T Map<T>(this Actor actor, object obj)
    {
      IMapper mapper = actor.GetService<IMapper>();
      return mapper.Map<T>(obj);
    }

    public static IToDoRepository Repository(this Actor actor)
    => actor.GetService<IToDoRepository>();
  }
}
