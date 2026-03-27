using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using futebolAPI.Data;
using futebolAPI.Models;

namespace futebolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly AppDbContext _db;

    public PlayersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("local")]
    public async Task<IActionResult> GetLocalPlayers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        // Conta o total de registros no banco
        var totalPlayers = await _db.Players.CountAsync();

        // Pula os registros das páginas anteriores e pega apenas a quantidade da página atual
        var players = await _db.Players
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new
        {
            TotalCount = totalPlayers,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalPlayers / (double)pageSize),
            Items = players
        };

        return players.Any() ? Ok(result) : NotFound("Nenhum jogador na página informada.");
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchPlayers([FromQuery] string? name, [FromQuery] string? position)
    {
        var query = _db.Players.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));

        if (!string.IsNullOrWhiteSpace(position))
            query = query.Where(p => p.Position.ToLower() == position.ToLower());

        var players = await query.ToListAsync();
        return players.Any() ? Ok(players) : NotFound("Nenhum jogador encontrado.");
    }

    [HttpPut("local/{id}")]
    public async Task<IActionResult> UpdatePlayer(int id, [FromBody] Player updatedPlayer)
    {
        var player = await _db.Players.FindAsync(id);
        if (player is null) return NotFound("Jogador não encontrado.");

        player.Name = updatedPlayer.Name;
        player.Age = updatedPlayer.Age;
        player.Number = updatedPlayer.Number;
        player.Position = updatedPlayer.Position;
        player.Photo = updatedPlayer.Photo;

        await _db.SaveChangesAsync();
        return Ok(player);
    }

    [HttpDelete("local/{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        var player = await _db.Players.FindAsync(id);
        if (player is null) return NotFound("Jogador não encontrado.");

        _db.Players.Remove(player);
        await _db.SaveChangesAsync();
        return Ok("Jogador removido com sucesso.");
    }
}