namespace TodoApi.Model.Dto;

public class TodoItemDTO
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public bool IsComplete { get; set; }
}
