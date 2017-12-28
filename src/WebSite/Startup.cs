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
  using U2U.AspNetCore.ScreenPlay;
  using Microsoft.AspNetCore.Authentication.Cookies;
  using Microsoft.AspNetCore.Authentication.OpenIdConnect;
  using Microsoft.AspNetCore.Authorization;
  using WebSite.Authorization;

  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public virtual void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(sharedOptions =>
      {
        sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        sharedOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      })
      .AddCookie(); //config => config;

      services.AddMvc(options => { });
      // services.AddAutoMapper();

      services.AddSingleton<IMapper>(
        new Mapper(new MapperConfiguration(cfg
      => cfg.AddProfile(new MappingProfile())))
      );

      var autoConfigOptions = new AutoConfigOptions();
      Configuration.Bind("AutoConfig", autoConfigOptions);
      services.AddAutoConfig(autoConfigOptions, key => Configuration.GetConnectionString(key));

      services.AddSingleton<IAuthorizationHandler, ClaimAuthorizationHandler>();
      services.AddAuthorization(options =>
      {
        options.AddPolicy("CanListClaims",
          policy => policy.AddRequirements(new ClaimRequirement("Claims", "list")));
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseFakeClaims();
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }
      
      app.UseAuthentication();

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
