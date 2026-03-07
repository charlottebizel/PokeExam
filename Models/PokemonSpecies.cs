// Données d'espèce — retournées par /api/v2/pokemon-species/{nom}
// On l'utilise pour récupérer le nom et la description en français
using System.Text.Json.Serialization;

namespace PokeBlazor.Models;

public class PokemonSpecies
{
    [JsonPropertyName("names")]
    public List<NameEntry> Names { get; set; } = new();

    [JsonPropertyName("flavor_text_entries")]
    public List<FlavorTextEntry> FlavorTextEntries { get; set; } = new();
}

// Nom traduit dans une langue donnée
public class NameEntry
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    [JsonPropertyName("language")]
    public Language Language { get; set; } = new();
}

// Description (flavor text) dans une langue donnée
public class FlavorTextEntry
{
    [JsonPropertyName("flavor_text")]
    public string FlavorText { get; set; } = "";
    [JsonPropertyName("language")]
    public Language Language { get; set; } = new();
}

public class Language { [JsonPropertyName("name")] public string Name { get; set; } = ""; }
