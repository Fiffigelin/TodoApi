namespace TodoApi.Model.Entity;

public class TodoItem
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public bool IsComplete { get; set; }
  public string? Secret { get; set; }

}
