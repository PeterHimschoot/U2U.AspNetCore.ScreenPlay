namespace WebSite.Authorization
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.Extensions.Logging;

  public class ClaimAuthorizationHandler :
    AuthorizationHandler<ClaimRequirement>,
    IAuthorizationHandler // Don't forget to mark with this interface!
                          // And don't forget to add to DI
  {
    private ILogger<ClaimAuthorizationHandler> logger;
    
    public ClaimAuthorizationHandler(ILogger<ClaimAuthorizationHandler> logger) {
      this.logger = logger;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
    {
      IEnumerable<Claim> claims = context.User.FindAll(requirement.Key);
      if (claims.Any(c => c.Value.Contains(requirement.Value)))
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }
}
