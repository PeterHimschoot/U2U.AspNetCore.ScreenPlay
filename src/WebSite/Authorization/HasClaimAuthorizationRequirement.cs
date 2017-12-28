namespace WebSite.Authorization
{
  using System.Security.Claims;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authorization;

  public class ClaimAuthorizationHandler :
    AuthorizationHandler<ClaimRequirement>,
    IAuthorizationHandler // Don't forget to mark with this interface!
                          // And don't forget to add to DI
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
    {
      Claim c = context.User.FindFirst(requirement.Key);
      if (c != null && c.Value.Contains(requirement.Value))
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }
}
