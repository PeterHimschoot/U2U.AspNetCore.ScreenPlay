﻿namespace U2U.AspNetCore.ScreenPlay
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

    public Actor CanUse<T>()
    {
      IAbility ability = (IAbility)this.Ability<T>();
      return CanUse(ability);
    }

    public Actor And() => this;

    public T GetAbility<T>() => this.Abilities.OfType<T>().SingleOrDefault();

    public bool HasBrowser
    => this.Abilities.OfType<Browser>().Any();

    public Browser UsesBrowser => this.Abilities.OfType<Browser>().Single();

    public Browser Browser() => UsesBrowser;
    
    public ApiClient ApiClient() => GetAbility<ApiClient>();

    public T Ability<T>() 
    => this.GetAbility<HttpClient>().Server.Host.Services.GetService<T>();
    
  }
}
