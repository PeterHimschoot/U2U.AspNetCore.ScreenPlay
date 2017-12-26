namespace U2U.AspNetCore.ScreenPlay
{
  using System;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Options;
  using U2U.AspNetCore.ScreenPlay.Identity;
  
  public static class InstallFakeClaimsExtensions
  {
    public static readonly InstallFakeClaimsOptions DefaultOptions =
    new InstallFakeClaimsOptions { CookieName = "FakeClaims", SkipEnvironmentCheck = false };
    public static IApplicationBuilder UseFakeClaims(this IApplicationBuilder builder, InstallFakeClaimsOptions options)
    => builder.UseMiddleware<InstallFakeClaimsMiddleware>(Options.Create(options));

    public static IApplicationBuilder UseFakeClaims(this IApplicationBuilder builder)
    => builder.UseFakeClaims(DefaultOptions);
  }
}
