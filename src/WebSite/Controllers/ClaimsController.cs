namespace WebSite
{
  using System.Collections.Generic;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using WebSite.ViewModels.Claims;

  public class ClaimsController : Controller
  {
    // [Route("")]
    [HttpGet("claims")]
    [Authorize(Policy = "CanListItems")]
    public ActionResult ShowClaims()
    {
      var vm = new ShowViewModel();
      vm.Claims = new List<string>();
      foreach (var claim in this.User.Claims)
      {
        vm.Claims.Add($"ClaimType = {claim.Type}, Value = {claim.Value}");
      }
      return View(vm);
    }
  }
}
