using Habbit_Api.Models;
using Habbit_Api.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IDatabaseService _db;

    public UsersController(IDatabaseService db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _db.Users.Find(_ => true).ToListAsync();
        return Ok(users);
    }

    [HttpGet("{auth0_id}")]
    public async Task<IActionResult> GetUserById(string auth0_id)
    {
        var user = await _db.Users.Find(u => u.Auth0Id == auth0_id).FirstOrDefaultAsync();
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User newUser)
    {
        if (newUser.Id == null)
        {
            newUser.Id = Guid.NewGuid().ToString(); // Генеруємо новий Id, якщо він не переданий
        }
        await _db.Users.InsertOneAsync(newUser);
        return CreatedAtAction(nameof(GetUserById), new { auth0_id = newUser.Auth0Id }, newUser);
    }

    [HttpPut("{auth0_id}")]
    public async Task<IActionResult> UpdateUser(string auth0_id, [FromBody] User updatedUser)
    {
        var result = await _db.Users.ReplaceOneAsync(u => u.Auth0Id == auth0_id, updatedUser);
        if (result.MatchedCount == 0) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _db.Users.DeleteOneAsync(u => u.Id == id);
        if (result.DeletedCount == 0) return NotFound();
        return NoContent();
    }
}