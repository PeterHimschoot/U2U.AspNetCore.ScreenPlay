namespace DSL_Tests
{
  using Core.Entities;
  using U2U.AspNetCore.ScreenPlay;
  using WebSite.ViewModels.ToDo;

  public static class InteractionExtensions
  {
    public static Interaction CouldGoToDefaultPage(this Interaction interaction)
      => interaction.Add(new Browses(Uris.Home));

    public static Interaction CouldGoToItemsPage(this Interaction interaction)
    => interaction.Add(new Browses(Uris.Items));

    public static Interaction WithToDoItems(this Interaction interaction, params ToDoItem[] items)
    {
      foreach (ToDoItem item in items)
      {
        interaction.Add(new AddToDoItem(item));
      }
      return interaction;
    }

    public static Interaction CouldGoToItemsCreate(this Interaction interaction) 
      => interaction.Add(new Browses(Uris.Create));

    public static Interaction EnterNewToDo(this Interaction interaction, CreateViewModel model) 
      => interaction.Add(new CreateToDoItem(model));
  }
}
