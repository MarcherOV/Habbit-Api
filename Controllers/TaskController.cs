using Habbit_Api.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


namespace Habbit_Api.Models;
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IDatabaseService _db;

    public TasksController(IDatabaseService db)
    {
        _db = db;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetTasksByUserId(string userId)
    {

        var filter = Builders<Task>.Filter.Eq("UserId", userId);
        var tasks = await _db.Tasks.Find(filter).ToListAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] Task newTask)
    {
        if (newTask == null)
            return BadRequest("Task is null.");

        if (string.IsNullOrEmpty(newTask.Id))
            newTask.Id = Guid.NewGuid().ToString();

        await _db.Tasks.InsertOneAsync(newTask);
        return CreatedAtAction(nameof(GetTasksByUserId), new { userId = newTask.UserId }, newTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(string id, [FromBody] Task updatedTask)
    {
        var result = await _db.Tasks.ReplaceOneAsync(t => t.Id.ToString() == id, updatedTask);
        if (result.MatchedCount == 0) return NotFound();
        return NoContent();
    }

    [HttpPut("complete/{id}")]
    public async Task<IActionResult> MarkTaskAsCompleted(string id)
    {
        var task = await _db.Tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
        if (task == null)
        {
            return NotFound();
        }

        task.CompletionDate = DateTime.UtcNow; // Встановлюємо поточну дату як дату завершення
        var result = await _db.Tasks.ReplaceOneAsync(t => t.Id == id, task);
        if (result.MatchedCount == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(string id)
    {
        var result = await _db.Tasks.DeleteOneAsync(t => t.Id.ToString() == id);
        if (result.DeletedCount == 0) return NotFound();
        return NoContent();
    }
}