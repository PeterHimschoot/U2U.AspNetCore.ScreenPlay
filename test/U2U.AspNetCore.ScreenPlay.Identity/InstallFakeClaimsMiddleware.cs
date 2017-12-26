using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace U2U.AspNetCore.ScreenPlay.Identity
{
  public class InstallFakeClaimsMiddleware
  {
    private RequestDelegate next;
    private ILogger<InstallFakeClaimsMiddleware> logger;
    private InstallFakeClaimsOptions options;

    public InstallFakeClaimsMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILogger<InstallFakeClaimsMiddleware> logger, IOptions<InstallFakeClaimsOptions> options)
    {
      this.next = next ?? throw new ArgumentNullException(nameof(next));
      this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
      this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
      
      if( !this.options.SkipEnvironmentCheck && !hostingEnv.IsDevelopment()) {
        throw new Exception("This middleware is for development only");
      }
    }

    public async Task Invoke(HttpContext context)
    {
      // Does the request contain the cookie with fake claims?
      var cookies = context.Request.Cookies;
      if(cookies.ContainsKey(this.options.CookieName)) {
        string value = default(string);
        cookies.TryGetValue(this.options.CookieName, out value);
        
        ClaimsPrincipal user = context.User;
        ClaimsIdentity identity = user.Identity as ClaimsIdentity;
        identity.AddClaim(new Claim(type: ClaimTypes.Country, value: "Belgium"));
      }
      await this.next.Invoke(context);
    }
  }
}
