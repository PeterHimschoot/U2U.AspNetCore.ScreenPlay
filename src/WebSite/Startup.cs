namespace WebSite
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using U2U.CleanArchitecture;
  using global::AutoMapper;
  using WebSite.AutoMapper;

  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      
      // Mapper.Initialize(config => config.AddProfile(new MappingProfile()));
      
    }

    public IConfiguration Configuration { get; }

    public virtual void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(options => { });
      services.AddAutoMapper();
      var autoConfigOptions = new AutoConfigOptions();
      Configuration.Bind("AutoConfig", autoConfigOptions);
      services.AddAutoConfig(autoConfigOptions, key => Configuration.GetConnectionString(key));
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller}/{action=Index}/{id?}");
      });
    }
  }
}
