using System.Text.Json.Serialization;

namespace futebolAPI.Models;

public class SquadResponse
{
    [JsonPropertyName("response")]
    public List<SquadData> Response { get; set; }
}

public class SquadData
{
    [JsonPropertyName("players")]
    public List<Player> Players { get; set; }
}

public class Player
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("number")]
    public int? Number { get; set; }

    [JsonPropertyName("position")]
    public string Position { get; set; }

    [JsonPropertyName("photo")]
    public string Photo { get; set; }

    // Chave Estrangeira para o Time
    public int TeamId { get; set; }

    [JsonIgnore]
    public Team Team { get; set; }
}