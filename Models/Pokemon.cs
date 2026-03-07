// ┌─────────────────────────────────────────────────────────────┐
// │  Modèles de données — correspondent au JSON de PokéAPI      │
// │  [JsonPropertyName] mappe le nom JSON → propriété C#        │
// └─────────────────────────────────────────────────────────────┘
using System.Text.Json.Serialization;

namespace PokeBlazor.Models;

// Pokémon principal retourné par /api/v2/pokemon/{nom}
public class Pokemon
{
    [JsonPropertyName("id")]     public int    Id     { get; set; }
    [JsonPropertyName("name")]   public string Name   { get; set; } = "";
    [JsonPropertyName("height")] public int    Height { get; set; }
    [JsonPropertyName("weight")] public int    Weight { get; set; }
    public string Description { get; set; } = "";
    [JsonPropertyName("sprites")] public Sprite           Sprites { get; set; } = new();
    [JsonPropertyName("types")]   public List<PokemonType> Types   { get; set; } = new();
    [JsonPropertyName("stats")]   public List<PokemonStat> Stats   { get; set; } = new();
}

// Image du Pokémon
public class Sprite      { [JsonPropertyName("front_default")] public string FrontDefault { get; set; } = ""; }

// Type (ex: feu, eau) — l'API retourne un objet imbriqué
public class PokemonType { [JsonPropertyName("type")] public TypeInfo Type { get; set; } = new(); }
public class TypeInfo    { [JsonPropertyName("name")] public string   Name { get; set; } = ""; }

// Statistique (PV, attaque, etc.)
public class PokemonStat { [JsonPropertyName("base_stat")] public int BaseStat { get; set; }
                           [JsonPropertyName("stat")]      public StatInfo Stat { get; set; } = new(); }
public class StatInfo    { [JsonPropertyName("name")] public string Name { get; set; } = ""; }
