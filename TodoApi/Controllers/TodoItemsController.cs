using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Model.Dto;
using TodoApi.Model.ViewModel;
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
    public async Task<ActionResult<IEnumerable<TodoItemViewModel>>> GetTodoItem()
    {
        return await _context.TodoItems
            .Select(x => ItemToViewModel(x))
            .ToListAsync();
    }

    // GET: api/TodoItem/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemViewModel>> GetTodoItem(Guid id)
    {
        var todoitem = await _context.TodoItems.FindAsync(id);

        if (todoitem == null)
        {
            return NotFound();
        }

        return ItemToViewModel(todoitem);
    }

    // PUT: api/TodoItem/{id}
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(Guid? id, PutTodoItemDTO todoDTO)
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
        todoItem.Description = todoDTO.Description;

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

        if (todo == null)
        {
            return BadRequest();
        }

        todo.IsComplete = !todo.IsComplete;
        await _context.SaveChangesAsync();

        return Ok(todo);
    }

    // POST: api/TodoItem
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TodoItemViewModel>> PostTodoItem(PostTodoItemDTO todoDTO)
    {
        var todoItem = new TodoItem
        {
            Name = todoDTO.Name,
            Description = todoDTO.Description,
            IsComplete = false
        };

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
          nameof(GetTodoItem),
          new { id = todoItem.Id },
          ItemToViewModel(todoItem));
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

    private static TodoItemViewModel ItemToViewModel(TodoItem todoItem)
    {
        return new TodoItemViewModel
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete
        };
    }
}
