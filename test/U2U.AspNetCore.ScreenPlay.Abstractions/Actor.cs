namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Microsoft.AspNetCore.TestHost;
  using Microsoft.Extensions.DependencyInjection;

  public class Actor
  {
    public string Name { get; }

    private Actor(string name)
    {
      this.Name = name;
    }

    public static Actor Named(string name)
    {
      return new Actor(name);
    }

    public List<IAbility> Abilities { get; } = new List<IAbility>();

    public Actor CanUse(IAbility use)
    {
      use = use ?? throw new ArgumentNullException(nameof(use));
      if (this.Abilities.Contains(use))
      {
        throw new ArgumentException(message: $"Actor {this.Name} already has ability {use.Name}");
      }
      else
      {
        this.Abilities.Add(use);
        return this;
      }
    }

    public Actor And() => this;

    public T GetAbility<T>() => this.Abilities.OfType<T>().SingleOrDefault();

    public T GetService<T>() 
    => TestServer.Host.Services.GetService<T>();
    
    public TestServer TestServer
    => this.GetAbility<IHttpClient>().Server;
  }
}
