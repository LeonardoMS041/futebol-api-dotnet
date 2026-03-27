using System.Text.Json.Serialization;

namespace futebolAPI.Models;

public class TeamResponse
{
    [JsonPropertyName("response")]
    public List<TeamData> Response { get; set; }
}

public class TeamData
{
    [JsonPropertyName("team")]
    public Team Team { get; set; }
}

public class Team
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("logo")]
    public string Logo { get; set; }

    // Relação: 1 Time tem Vários Jogadores
    [JsonIgnore]
    public List<Player> Players { get; set; } = new();
}