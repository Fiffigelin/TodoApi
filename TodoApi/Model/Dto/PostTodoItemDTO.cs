namespace TodoApi.Model.Dto;

public class PostTodoItemDTO
{
  public required string Name { get; set; }
  public string? Description { get; set; }
}
