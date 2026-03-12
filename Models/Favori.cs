// Modèle d'un favori sauvegardé en localStorage
namespace PokeBlazor.Models;

/// <summary>
/// Represents a favorite Pokémon saved in local storage.
/// </summary>
public class Favori
{
    /// <summary>
    /// Gets or sets the unique auto-incremented identifier for the favorite.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the Pokémon.
    /// </summary>
    public string NomPokemon { get; set; } = "";

    /// <summary>
    /// Gets or sets an optional comment for the favorite.
    /// </summary>
    public string? Commentaire { get; set; }

    /// <summary>
    /// Gets or sets the URL of the Pokémon's sprite for display.
    /// </summary>
    public string? ImageUrl { get; set; }
}
