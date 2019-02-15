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
using AutoMapper;

namespace DSL_Tests
{

  // https://reasoncodeexample.com/2016/08/29/how-to-keep-things-tidy-when-using-asp-net-core-testserver/

  public class ToDoItemsTests : DSLTest
  {
    public ToDoItemsTests(ITestOutputHelper logger) : base(logger) { }

    [Fact]
    public async Task UserOpensDefaultPage()
    {
      // Arrange
      var peter = Actor.Named("Peter").CanUse(Web.Browser<TestStartup>());
      // Act
      await Given.That(peter).CouldGoToDefaultPage().Successfully();
      // Assert
      peter.UsingBrowser().Should().HaveHtmlHeader("U2U Training")
           .And().Should().HaveContentType("text/html; charset=utf-8");
      //  .And().Should().HaveHeader("Cache-Control", "no-cache, no-store");
    }

    [Fact()]
    public async Task CreateNewItemShouldInsertIntoDatabaseToo()
    {
      var peter = Actor.Named("Peter").CanUse(Web.Browser<TestStartup>());
      var model = TestData.AddedItem;
      var viewModel = peter.Map<CreateViewModel>(TestData.AddedItem);
      await Given.That(peter).CouldGoToItemsCreate().And()
                      .EnterNewToDo(viewModel).Successfully();
      peter.UsingBrowser().Should().HaveStatusCode(HttpStatusCode.Redirect)
           .And().AddedToDoItem(model, peter.GetService<IToDoRepository>())
           .And().Committed(peter.Repository());
    }

    [Fact()]
    public async Task CreateNewItemShouldRedirectToItemsPage()
    {
      var peter = Actor.Named("Peter").CanUse(Web.Browser<TestStartup>());
      var model = TestData.AddedItem;
      var viewModel = peter.Map<CreateViewModel>(TestData.AddedItem);
      await Given.That(peter).CouldGoToItemsCreate().And()
                      .EnterNewToDo(viewModel).Successfully();
      peter.UsingBrowser().Should().HaveStatusCode(HttpStatusCode.Redirect, Uris.Items);
    }

    [Fact()]
    public async Task AUserShouldSeeOnlyTheirTasks()
    {
      // Arrange
      var peter = Actor.Named("Peter").CanUse(Web.Browser<TestStartup>());
      // Act
      await Given.That(peter).WithToDoItems(TestData.InitialToDos)
                 .And().CouldGoToItemsPage().Successfully();
      // Assert
      var items = TestData.InitialToDos.Select(item => item.Title);
      peter.UsingBrowser().Should().HaveToDoItems(items.ToArray());
    }
  }
}
