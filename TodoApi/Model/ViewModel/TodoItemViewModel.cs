namespace TodoApi.Model.ViewModel;

public class TodoItemViewModel
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public bool IsComplete { get; set; }
}
