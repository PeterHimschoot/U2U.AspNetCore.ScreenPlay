namespace DSL_Tests
{
  using Xunit;
  using System.Threading.Tasks;
  using System.Net;
  using U2U.AspNetCore.ScreenPlay;
  using Xunit.Abstractions;

  public class ApiTests : DSLTest
  {
    public ApiTests(ITestOutputHelper logger) : base(logger) { }

    [Fact]
    public async Task ShowItemsShouldReturnAllToDoItems()
    {
      var apiClient = Web.ApiClient<TestStartup>()
                       .WithAcceptJsonHeader()
                       .WithFakeClaimsPrincipal(TestPrincipals.FullClaimsPrincipal);
      var peter = Actor.Named("Peter").CanUse(apiClient);
      //  .And().CanUse<IToDoRepository>();
      await Given.That(peter).WithToDoItems(TestData.InitialToDos)
                 .And().CouldGet(Uris.ApiToDos).Successfully();
      peter.UsingApiClient().ShouldReturn().HaveStatusCode(HttpStatusCode.OK)
           .And().Should().HaveToDos(TestData.InitialToDos);
    }

    [Fact]
    public async Task InsertNewItemShouldAddItToRepository()
    {
      var apiClient = Web.ApiClient<TestStartup>()
                  .WithAcceptJsonHeader()
                  .WithFakeClaimsPrincipal(TestPrincipals.FullClaimsPrincipal);
      var peter = Actor.Named("Peter").CanUse(apiClient);
      await Given.That(peter).WithToDoItems(TestData.InitialToDos)
                 .And().CouldInsertToDoItem(TestData.AddedItem).Successfully();
      peter.UsingApiClient().ShouldReturn().HaveCreatedStatusCode();
    }

    [Fact]
    public async Task InsertNewItemShouldCallCommit()
    {
      var apiClient = Web.ApiClient<TestStartup>()
                  .WithAcceptJsonHeader()
                  .WithFakeClaimsPrincipal(TestPrincipals.FullClaimsPrincipal);
      var peter = Actor.Named("Peter").CanUse(apiClient);
      //  .And().CanUse<IToDoRepository>();
      await Given.That(peter).WithToDoItems(TestData.InitialToDos)
                 .And().CouldInsertToDoItem(TestData.AddedItem).Successfully();
      peter.UsingApiClient().ShouldReturn().CallCommit(peter.Repository());
    }
  }
}
