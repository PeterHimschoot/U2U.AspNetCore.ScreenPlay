using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace U2U_HRS_Infra
{
  public abstract class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T>
  where T : DbContext
  {
    private IConfiguration Configuration { get; }
    private string ConfigKey { get; }

    public DesignTimeDbContextFactory(string configKey)
    {
      this.ConfigKey = configKey ?? throw new ArgumentNullException(nameof(configKey));
      ConfigurationBuilder cb = new ConfigurationBuilder();
      cb.AddUserSecrets(Assembly.GetCallingAssembly())
        .AddEnvironmentVariables();
      Configuration = cb.Build();
    }

    public T CreateDbContext(string[] args)
    {
      var builder = new DbContextOptionsBuilder<T>();
      builder.UseSqlServer(Configuration.GetConnectionString(ConfigKey));
      return CreateDbContext(builder.Options);
    }

    protected abstract T CreateDbContext(DbContextOptions<T> options);
  }
}
