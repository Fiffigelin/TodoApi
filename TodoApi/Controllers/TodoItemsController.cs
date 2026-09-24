using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Model.Dto;
using TodoApi.Model.Entity;
using TodoApi.Models;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoContext _context;
    public TodoItemsController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/TodoItem
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItem()
    {
        return await _context.TodoItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    // GET: api/TodoItem/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItem(Guid id)
    {
        var todoitem = await _context.TodoItems.FindAsync(id);

        if (todoitem == null)
        {
            return NotFound();
        }

        return ItemToDTO(todoitem);
    }

    // PUT: api/TodoItem/{id}
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(Guid? id, TodoItemDTO todoDTO)
    {
        if (id != todoDTO.Id)
        {
            return BadRequest();
        }

        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        todoItem.Name = todoDTO.Name;
        todoItem.IsComplete = todoDTO.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("toggle-status/{id}")]
    public async Task<IActionResult> ToggleTodoStatus(Guid? id)
    {
      var todo = await _context.TodoItems.FindAsync(id);

      if (id != todo.Id)
      {
        return BadRequest();
      }

      todo.IsComplete = !todo.IsComplete;
      await _context.SaveChangesAsync();

      return Ok(200);
    }

  // POST: api/TodoItem
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
    {
        var todoItem = new TodoItem
        {
            IsComplete = todoDTO.IsComplete,
            Name = todoDTO.Name,
            Description = todoDTO.Description
        };

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
          nameof(GetTodoItem),
          new { id = todoItem.Id },
          ItemToDTO(todoItem));
    }

    // DELETE: api/TodoItem/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(Guid? id)
    {
        var todoitem = await _context.TodoItems.FindAsync(id);
        if (todoitem == null)
        {
            return NotFound();
        }

        _context.TodoItems.Remove(todoitem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(System.Guid? id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }

    private static TodoItemDTO ItemToDTO(TodoItem todoItem)
    {
        return new TodoItemDTO
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete
        };
    }
}
