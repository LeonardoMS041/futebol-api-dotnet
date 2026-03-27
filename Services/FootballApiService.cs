using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using futebolAPI.Models;
using futebolAPI.Data;

namespace futebolAPI.Services;

public class FootballApiService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    public FootballApiService(IHttpClientFactory httpClientFactory, AppDbContext context)
    {
        _httpClient = httpClientFactory.CreateClient("ApiFootball");
        _context = context;
    }

    public async Task<TeamResponse?> GetTeamAsync(int teamId)
    {
        var response = await _httpClient.GetAsync($"teams?id={teamId}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TeamResponse>(content);
    }

    public async Task<SquadResponse?> GetSquadAsync(int teamId)
    {
        var response = await _httpClient.GetAsync($"players/squads?team={teamId}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SquadResponse>(content);
    }

    public async Task<string> SyncSquadToDbAsync(int teamId)
    {
        // 1. Salva o time se ainda não existir no banco
        if (!await _context.Teams.AnyAsync(t => t.Id == teamId))
        {
            var teamResponse = await GetTeamAsync(teamId);
            var team = teamResponse?.Response?.FirstOrDefault()?.Team;
            if (team != null)
            {
                _context.Teams.Add(team);
                await _context.SaveChangesAsync(); // Salva para gerar a relação
            }
        }

        // 2. Busca e salva os jogadores vinculando ao time
        var squadResponse = await GetSquadAsync(teamId);
        var players = squadResponse?.Response?.FirstOrDefault()?.Players;

        if (players == null || !players.Any()) return "Nenhum jogador encontrado na API.";

        foreach (var player in players)
        {
            player.TeamId = teamId; // Informa a qual time o jogador pertence

            if (!await _context.Players.AnyAsync(p => p.Id == player.Id))
            {
                _context.Players.Add(player);
            }
        }

        await _context.SaveChangesAsync();
        return $"{players.Count} jogadores processados e salvos no banco de dados!";
    }
}