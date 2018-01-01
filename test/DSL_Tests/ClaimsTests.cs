using System;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using WebSite;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore;
using U2U.AspNetCore.ScreenPlay;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Core.Interfaces;
using Xunit.Abstractions;
using AngleSharp.Parser.Html;
using AngleSharp.Dom.Html;
using Core.Entities;
using System.Net.Http.Headers;
using System.IO;
using WebSite.ViewModels.ToDo;
using System.Collections.Generic;
using System.Security.Claims;

namespace DSL_Tests
{

  // https://reasoncodeexample.com/2016/08/29/how-to-keep-things-tidy-when-using-asp-net-core-testserver/

  public class ClaimsTests : DSLTest
  {
    public ClaimsTests(ITestOutputHelper logger) :base(logger) {}

    [Fact]
    public async Task ShowClaimsShouldReturnFakeClaims()
    {
      var principal = TestPrincipals.FullClaimsPrincipal;
      Assert.True(principal.Identity.IsAuthenticated);
      var claimStrings =
        principal.Claims.Select(item => $"ClaimType = {item.Type}, Value = {item.Value}");
      var browser = Web.Browser<TestStartup>()
                       .WithFakeClaimsPrincipal(principal);
      var peter = Actor.Named("Peter").CanUse(browser);
      await Given.That(peter).CouldGoToPage(Uris.Claims).Successfully();
      peter.Browser().Should().HaveStatusCode(HttpStatusCode.OK)
        .And().Should().HaveContentsWith("tr>td", claimStrings.ToArray());
    }

    [Fact]
    public async Task ShowClaimsWithMissingClaimsShouldRedirectToLogin()
    {
      var browser = Web.Browser<TestStartup>();
      var peter = Actor.Named("Peter").CanUse(browser);
      await Given.That(peter).CouldGoToPage(Uris.Claims).Successfully();
      peter.Browser().Should().HaveStatusCode(HttpStatusCode.Redirect);
    }
  }
}
