using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using futebolAPI.Data;
using futebolAPI.Services;

namespace futebolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly FootballApiService _service;
    private readonly AppDbContext _db;

    public TeamsController(FootballApiService service, AppDbContext db)
    {
        _service = service;
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeam(int id)
    {
        var data = await _service.GetTeamAsync(id);
        return data is not null ? Ok(data) : NotFound();
    }

    [HttpGet("{id}/squad")]
    public async Task<IActionResult> GetSquad(int id)
    {
        var data = await _service.GetSquadAsync(id);
        return data is not null ? Ok(data) : NotFound();
    }

    [HttpPost("{id}/sync")]
    public async Task<IActionResult> SyncSquad(int id)
    {
        var result = await _service.SyncSquadToDbAsync(id);
        return Ok(result);
    }

    [HttpGet("local")]
    public async Task<IActionResult> GetLocalTeams()
    {
        var teams = await _db.Teams.Include(t => t.Players).ToListAsync();
        return teams.Any() ? Ok(teams) : NotFound("Nenhum time no banco.");
    }
}