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

    // Отримання Strength
    [HttpGet("{auth0_id}/strength")]
    public async Task<IActionResult> GetStrength(string auth0_id)
    {
        var user = await _db.Users.Find(u => u.Auth0Id == auth0_id).FirstOrDefaultAsync();
        if (user == null) return NotFound();

        return Ok(new { Strength = user.Stats.СurrentProgressStrengh });
    }

    // Отримання Intelligence
    [HttpGet("{auth0_id}/intelligence")]
    public async Task<IActionResult> GetIntelligence(string auth0_id)
    {
        var user = await _db.Users.Find(u => u.Auth0Id == auth0_id).FirstOrDefaultAsync();
        if (user == null) return NotFound();

        return Ok(new { Intelligence = user.Stats.СurrentProgressIntelligence });
    }

    // Отримання Charisma
    [HttpGet("{auth0_id}/charisma")]
    public async Task<IActionResult> GetCharisma(string auth0_id)
    {
        var user = await _db.Users.Find(u => u.Auth0Id == auth0_id).FirstOrDefaultAsync();
        if (user == null) return NotFound();

        return Ok(new { Charisma = user.Stats.СurrentProgressCharisma });
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

    // Оновлення Strength
    [HttpPut("{auth0_id}/strength")]
    public async Task<IActionResult> UpdateStrength(string auth0_id, [FromBody] double strengthProgress)
    {
        var user = await _db.Users.Find(u => u.Auth0Id == auth0_id).FirstOrDefaultAsync();
        if (user == null) return NotFound();

        user.Stats.СurrentProgressStrengh = strengthProgress;

        var result = await _db.Users.ReplaceOneAsync(u => u.Auth0Id == auth0_id, user);

        if (result.ModifiedCount > 0) return Ok(user);

        return BadRequest("Failed to update Strength progress.");
    }

    // Оновлення Intelligence
    [HttpPut("{auth0_id}/intelligence")]
    public async Task<IActionResult> UpdateIntelligence(string auth0_id, [FromBody] double intelligenceProgress)
    {
        var user = await _db.Users.Find(u => u.Auth0Id == auth0_id).FirstOrDefaultAsync();
        if (user == null) return NotFound();

        user.Stats.СurrentProgressIntelligence = intelligenceProgress;

        var result = await _db.Users.ReplaceOneAsync(u => u.Auth0Id == auth0_id, user);

        if (result.ModifiedCount > 0) return Ok(user);

        return BadRequest("Failed to update Intelligence progress.");
    }

    // Оновлення Charisma
    [HttpPut("{auth0_id}/charisma")]
    public async Task<IActionResult> UpdateCharisma(string auth0_id, [FromBody] double charismaProgress)
    {
        var user = await _db.Users.Find(u => u.Auth0Id == auth0_id).FirstOrDefaultAsync();
        if (user == null) return NotFound();

        user.Stats.СurrentProgressCharisma = charismaProgress;

        var result = await _db.Users.ReplaceOneAsync(u => u.Auth0Id == auth0_id, user);

        if (result.ModifiedCount > 0) return Ok(user);

        return BadRequest("Failed to update Charisma progress.");
    }

    [HttpPost("{auth0_id}/update-progress")]
    public async Task<IActionResult> UpdateProgress(string auth0_id, [FromBody] UpdateProgressRequest request)
    {
        var user = await _db.Users.Find(u => u.Auth0Id == auth0_id).FirstOrDefaultAsync();
        if (user == null) return NotFound();

        // Оновлення відповідного атрибуту
        switch (request.Attribute)
        {
            case Habbit_Api.Models.Attribute.Strength:
                user.Stats.СurrentProgressStrengh += request.Increment;
                break;
            case Habbit_Api.Models.Attribute.Intelligence:
                user.Stats.СurrentProgressIntelligence += request.Increment;
                break;
            case Habbit_Api.Models.Attribute.Charisma:
                user.Stats.СurrentProgressCharisma += request.Increment;
                break;
            default:
                return BadRequest("Invalid attribute.");
        }

        // Зберігаємо зміни
        await _db.Users.ReplaceOneAsync(u => u.Auth0Id == auth0_id, user);
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