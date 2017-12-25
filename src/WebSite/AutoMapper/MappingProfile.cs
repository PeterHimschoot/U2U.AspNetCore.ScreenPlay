namespace WebSite.AutoMapper {
  using global::AutoMapper;

  public class MappingProfile : Profile {
    public MappingProfile() {
        // Add as many of these lines as you need to map your objects
        CreateMap<Core.Entities.ToDoItem, ViewModels.ToDo.EditViewModel>().ReverseMap();
    }
}
}
