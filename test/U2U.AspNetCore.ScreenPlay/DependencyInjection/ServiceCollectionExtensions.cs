namespace U2U.AspNetCore.ScreenPlay
{

  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.DependencyInjection.Extensions;
  
  public static class ServiceCollectionExtensions
  {

    static IServiceCollection Replace<I, C>(this IServiceCollection services, ServiceLifetime lifeTime)
    {
      var descriptor = new ServiceDescriptor(typeof(I), typeof(C), lifeTime);
      return services.Replace(descriptor);
    }
    
    public static IServiceCollection ReplaceSingleton<I,C>(this IServiceCollection services)
    => services.Replace<I,C>(lifeTime: ServiceLifetime.Singleton);
    
    public static IServiceCollection ReplaceTransient<I,C>(this IServiceCollection services)
    => services.Replace<I,C>(lifeTime: ServiceLifetime.Transient);
    
    public static IServiceCollection ReplaceScoped<I,C>(this IServiceCollection services)
    => services.Replace<I,C>(lifeTime: ServiceLifetime.Scoped);
    
  }
}
