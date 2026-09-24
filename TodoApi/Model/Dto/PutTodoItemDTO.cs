namespace TodoApi.Model.Dto;

public class PutTodoItemDTO
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
}
