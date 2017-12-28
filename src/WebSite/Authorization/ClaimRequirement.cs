namespace WebSite.Authorization
{
  using Microsoft.AspNetCore.Authorization;

  public class ClaimRequirement : IAuthorizationRequirement
  {
    public string Key { get; set; }
    public string Value { get; set; }

    public ClaimRequirement(string key, string value)
    {
      this.Key = key;
      this.Value = value;
    }
  }
}
