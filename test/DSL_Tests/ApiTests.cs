namespace DSL_Tests
{
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

  public class ApiTests : DSLTest
  {
    public ApiTests(ITestOutputHelper logger) : base(logger) { }

    [Fact]
    public async Task ShowItemsShouldReturnAllToDoItems()
    {
      var apiClient = Web.ApiClient<TestStartup>()
                       .WithAcceptJsonHeader()
                       .WithFakeClaimsPrincipal(TestPrincipals.FullClaimsPrincipal);
      var peter = Actor.Named("Peter").CanUse(apiClient)
                       .And().CanUse<IToDoRepository>();
      await Given.That(peter).HasToDoItems(TestData.InitialToDos)
                 .And().CouldGet(Uris.ApiToDos).Successfully();
      peter.ApiClient().ShouldReturn().HaveStatusCode(HttpStatusCode.OK)
           .And().Should().HaveToDos(TestData.InitialToDos);
    }
  }
}
