namespace U2U.AspNetCore.ScreenPlay.Identity
{
  using System;
  using System.Collections.Generic;
  using System.Security.Claims;
  using Microsoft.AspNetCore.Authentication;
  using Newtonsoft.Json;

  public class ClaimsSerializer
  {
    // https://github.com/aspnet/Security
    // https://github.com/aspnet/Security/blob/99aa3bd35dd5fbe46a93eef8a2c8ab1f9fe8d05b/src/Microsoft.AspNetCore.Authentication/Data/TicketSerializer.cs
    // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.authenticationticket?view=aspnetcore-2.0
    // https://stormpath.com/blog/token-authentication-asp-net-core

    public string SerializeTicket(AuthenticationTicket ticket)
    {
      var ser = TicketSerializer.Default;
      var bytes = ser.Serialize(ticket);
      return Convert.ToBase64String(bytes);
    }

    public AuthenticationTicket DeserializeTicket(string s)
    {
      var ser = TicketSerializer.Default;
      var bytes = Convert.FromBase64String(s);
      var result = ser.Deserialize(bytes);
      return result;
    }
  }
}
