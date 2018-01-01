namespace WebSite.Authorization
{
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
      Claim c = context.User.FindFirst(requirement.Key);
      if( c == null ) {
        logger.LogWarning($"######### No claim {requirement.Key} could be found");
      }
      if (c != null && c.Value.Contains(requirement.Value))
      {
        logger.LogWarning($"######### Claim {requirement.Key} has value {c.Value}");
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }
}
