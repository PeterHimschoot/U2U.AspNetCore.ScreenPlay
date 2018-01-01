namespace U2U.AspNetCore.ScreenPlay
{

  using System.Security.Claims;

  public class TestPrincipals
  {

    public static Claim CanListClaims = new Claim("Claims", "list");
    public static Claim CanListItems = new Claim("Items", "list");
    public static Claim CanModifyItems = new Claim("Items", "modify");

    public static ClaimsIdentity NoClaimsIdentity;
    public static ClaimsIdentity FullClaimsIdentity;
    public static ClaimsPrincipal NoClaimsPrincipal;
    public static ClaimsPrincipal FullClaimsPrincipal;

    static TestPrincipals()
    {
      NoClaimsIdentity = new ClaimsIdentity();
      FullClaimsIdentity = new ClaimsIdentity("FullClaims");
      FullClaimsIdentity.AddClaim(CanListClaims);
      FullClaimsIdentity.AddClaim(CanListItems);
      FullClaimsIdentity.AddClaim(CanModifyItems);

      NoClaimsPrincipal = new ClaimsPrincipal(NoClaimsIdentity);
      FullClaimsPrincipal = new ClaimsPrincipal(FullClaimsIdentity);
    }
  }
}
