using System;
using System.Collections.Generic;
using System.Linq;

namespace U2U.AspNetCore.ScreenPlay
{
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

    public List<Ability> Abilities { get; } = new List<Ability>();

    public Actor CanUse(Ability use)
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
    
    public Browser UsesBrowser => this.Abilities.OfType<Browser>().Single();
    
    public Browser Browser() => UsesBrowser;
  }
}
