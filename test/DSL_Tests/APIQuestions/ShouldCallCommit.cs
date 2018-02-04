using System;
using Core.Interfaces;
using U2U.AspNetCore.ScreenPlay;

namespace DSL_Tests
{  
  public class ApiShouldCallCommit : ApiQuestion
  {
    IToDoRepository repository;
    public ApiShouldCallCommit(IToDoRepository repository)
    {
      this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected override ApiClient Assert(ApiClient client)
    {
      FakeToDoRepository repo = this.repository as FakeToDoRepository;
      Xunit.Assert.NotNull(repo);
      Xunit.Assert.True(repo.CommitCalled);
      return client;
    }
  }
}
