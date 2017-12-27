namespace U2U.AspNetCore.ScreenPlay.Tests
{

  using Xunit;
  using U2U.AspNetCore.ScreenPlay.Identity;
  using System.Collections.Generic;
  using System.Security.Claims;
  using Microsoft.AspNetCore.Authentication;
  using System.Linq;

  public class ClaimsSerializerShould
  {

    private IEnumerable<Claim> claims = new List<Claim> {
     new Claim("X", "Y"),
     new Claim("A", "B")
   };

    [Fact]
    public void DeserializeSameClaimsAfterSerialization()
    {
      var identity = new ClaimsIdentity();
      identity.AddClaim(new Claim("X", "Y"));
      var identities = new List<ClaimsIdentity> { identity };
      var user = new ClaimsPrincipal(identities);
      var ticket = new AuthenticationTicket(user, "test");
      var serializer = new ClaimsSerializer();
      var test = serializer.SerializeTicket(ticket);
      
      var newTicket = serializer.DeserializeTicket(test);
      Assert.Equal(1, newTicket.Principal.Identities.First().Claims.Count());
    }
  }
}
