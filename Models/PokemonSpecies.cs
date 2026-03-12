// Données d'espèce — retournées par /api/v2/pokemon-species/{nom}
// On l'utilise pour récupérer le nom et la description en français
using System.Text.Json.Serialization;

namespace PokeBlazor.Models;

/// <summary>
/// Represents the species data for a Pokémon, used to get translated names and descriptions.
/// </summary>
public class PokemonSpecies
{
    /// <summary>
    /// Gets or sets the list of names for the Pokémon in different languages.
    /// </summary>
    [JsonPropertyName("names")]
    public List<NameEntry> Names { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of flavor text entries (descriptions) for the Pokémon in different languages.
    /// </summary>
    [JsonPropertyName("flavor_text_entries")]
    public List<FlavorTextEntry> FlavorTextEntries { get; set; } = new();
}

/// <summary>
/// Represents a translated name in a specific language.
/// </summary>
public class NameEntry
{
    /// <summary>
    /// Gets or sets the translated name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Gets or sets the language of the translated name.
    /// </summary>
    [JsonPropertyName("language")]
    public Language Language { get; set; } = new();
}

/// <summary>
/// Represents a flavor text (description) in a specific language.
/// </summary>
public class FlavorTextEntry
{
    /// <summary>
    /// Gets or sets the flavor text.
    /// </summary>
    [JsonPropertyName("flavor_text")]
    public string FlavorText { get; set; } = "";

    /// <summary>
    /// Gets or sets the language of the flavor text.
    /// </summary>
    [JsonPropertyName("language")]
    public Language Language { get; set; } = new();
}

/// <summary>
/// Represents a language.
/// </summary>
public class Language { 
    /// <summary>
    /// Gets or sets the name of the language (e.g., "fr", "en").
    /// </summary>
    [JsonPropertyName("name")] public string Name { get; set; } = ""; 
}
