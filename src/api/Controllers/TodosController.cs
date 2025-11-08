using EzraTask.Api.Models;
using EzraTask.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EzraTask.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public IActionResult GetTodos([FromQuery] PaginationQueryDto query, [FromQuery] bool isArchived = false)
    {
        var paginatedResponse = _todoService.GetAll(query.PageNumber, query.PageSize, isArchived);
        return Ok(paginatedResponse);
    }

    [HttpPost]
    public IActionResult CreateTodo(CreateTodoDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        var newTodo = _todoService.Create(createDto);
        var todoDto = newTodo.ToDto();

        return CreatedAtAction(nameof(GetTodoById), new { id = newTodo.Id }, todoDto);
    }

    [HttpGet("{id:long}")]
    public IActionResult GetTodoById(long id)
    {
        var todo = _todoService.GetById(id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo.ToDto());
    }

    [HttpPatch("{id:long}/toggle-completion")]
    public IActionResult ToggleCompletion(long id)
    {
        var updatedTodo = _todoService.ToggleCompletion(id);
        if (updatedTodo == null)
        {
            return NotFound();
        }
        return Ok(updatedTodo.ToDto());
    }

    [HttpPatch("{id:long}/archive")]
    public IActionResult ArchiveTodo(long id)
    {
        var success = _todoService.Archive(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
