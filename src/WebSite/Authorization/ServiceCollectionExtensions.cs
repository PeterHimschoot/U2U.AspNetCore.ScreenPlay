namespace WebSite.Authorization
{
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.Extensions.DependencyInjection;

  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    => services.AddSingleton<IAuthorizationHandler, ClaimAuthorizationHandler>()
               .AddAuthorization(options =>
               {
                 options.AddPolicy("CanListClaims",
                   policy => policy.AddRequirements(new ClaimRequirement("Claims", "list")));
               });
  }
}
