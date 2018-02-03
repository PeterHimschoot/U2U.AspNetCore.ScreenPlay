
using System;
using Core.Interfaces;
using U2U.AspNetCore.ScreenPlay;

namespace DSL_Tests
{
  public class ShouldCallCommit : Question
  {
    IToDoRepository repository;
    public ShouldCallCommit(IToDoRepository repository)
    {
      this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    protected override Browser Assert(Browser browser)
    {
      FakeToDoRepository repo = this.repository as FakeToDoRepository;
      Xunit.Assert.NotNull(repo);
      Xunit.Assert.True(repo.CommitCalled);
      return browser;
    }
  }
}
