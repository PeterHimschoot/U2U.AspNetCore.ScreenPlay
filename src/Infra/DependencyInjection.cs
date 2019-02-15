namespace Microsoft.Extensions.DependencyInjection
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Net.Http;
  // using Microsoft.EntityFrameworkCore;
  using U2U.CleanArchitecture;
  using Infra.DbContexts;
  using Infra.Repositories;
  using Core.Interfaces;
  using Microsoft.EntityFrameworkCore;

  // This class allows the dependent project to configure dependencies

  [AutoConfig]
  public static class DependencyInjection
  {
    [AutoConfig(ToDoDb.ConnectionStringName)]
    public static IServiceCollection AddToDoDb(this IServiceCollection services, string connectionString, [MigrationAssembly] string migrationAssembly)
      => services.AddDbContext<ToDoDb>(optionsBuilder =>
           optionsBuilder.UseSqlServer(connectionString,
             sqlServerOptions => sqlServerOptions.MigrationsAssembly(migrationAssembly)));

    [AutoConfig]
    public static IServiceCollection AddToDoRepository(this IServiceCollection services)
    => services.AddScoped<IToDoRepository, ToDoRepository>();

  }
}
