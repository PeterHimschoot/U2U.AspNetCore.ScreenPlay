namespace U2U.AspNetCore.ScreenPlay
{
using System;
using System.Collections.Generic;
using System.Linq;
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
    
    public Actor CanUse<T>() {
      IAbility ability = (IAbility) this.Ability<T>(); 
      return CanUse(ability);
    }

    public Actor And() => this;

    public T GetAbility<T>() => this.Abilities.OfType<T>().Single();

    public Browser UsesBrowser => this.Abilities.OfType<Browser>().Single();

    public Browser Browser() => UsesBrowser;
    
    public T Ability<T>() => this.Browser().Server.Host.Services.GetService<T>(); 
  }
}
