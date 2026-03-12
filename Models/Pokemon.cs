// ┌─────────────────────────────────────────────────────────────┐
// │  Modèles de données — correspondent au JSON de PokéAPI      │
// │  [JsonPropertyName] mappe le nom JSON → propriété C#        │
// └─────────────────────────────────────────────────────────────┘
using System.Text.Json.Serialization;

namespace PokeBlazor.Models;

/// <summary>
/// Represents a Pokémon, with data retrieved from the PokéAPI.
/// </summary>
public class Pokemon
{
    /// <summary>
    /// Gets or sets the Pokémon's ID.
    /// </summary>
    [JsonPropertyName("id")] public int Id { get; set; }

    /// <summary>
    /// Gets or sets the Pokémon's name.
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets the Pokémon's height.
    /// </summary>
    [JsonPropertyName("height")] public int Height { get; set; }

    /// <summary>
    /// Gets or sets the Pokémon's weight.
    /// </summary>
    [JsonPropertyName("weight")] public int Weight { get; set; }

    /// <summary>
    /// Gets or sets the Pokémon's description.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Gets or sets the Pokémon's sprites.
    /// </summary>
    [JsonPropertyName("sprites")] public Sprite Sprites { get; set; } = new();

    /// <summary>
    /// Gets or sets the Pokémon's types.
    /// </summary>
    [JsonPropertyName("types")] public List<PokemonType> Types { get; set; } = new();

    /// <summary>
    /// Gets or sets the Pokémon's stats.
    /// </summary>
    [JsonPropertyName("stats")] public List<PokemonStat> Stats { get; set; } = new();
}

/// <summary>
/// Represents the sprites (images) for a Pokémon.
/// </summary>
public class Sprite { 
    /// <summary>
    /// Gets or sets the URL for the front default sprite.
    /// </summary>
    [JsonPropertyName("front_default")] public string FrontDefault { get; set; } = ""; 
}

/// <summary>
/// Represents a Pokémon's type (e.g., Fire, Water).
/// </summary>
public class PokemonType { 
    /// <summary>
    /// Gets or sets the type information.
    /// </summary>
    [JsonPropertyName("type")] public TypeInfo Type { get; set; } = new(); 
}

/// <summary>
/// Represents the name of a Pokémon's type.
/// </summary>
public class TypeInfo { 
    /// <summary>
    /// Gets or sets the name of the type.
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = ""; 
}

/// <summary>
/// Represents a Pokémon's statistic (e.g., HP, Attack).
/// </summary>
public class PokemonStat { 
    /// <summary>
    /// Gets or sets the base value of the statistic.
    /// </summary>
    [JsonPropertyName("base_stat")] public int BaseStat { get; set; }

    /// <summary>
    /// Gets or sets the statistic's information.
    /// </summary>
    [JsonPropertyName("stat")] public StatInfo Stat { get; set; } = new(); 
}

/// <summary>
/// Represents the name of a Pokémon's statistic.
/// </summary>
public class StatInfo { 
    /// <summary>
    /// Gets or sets the name of the statistic.
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = ""; 
}
