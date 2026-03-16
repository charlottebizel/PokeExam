// Modèle d'un dresseur sauvegardé en localStorage
namespace PokeBlazor.Models;

/// <summary>
/// Represents a trainer account saved in local storage.
/// </summary>
public class Dresseur
{
    /// <summary>
    /// Gets or sets the username of the trainer.
    /// </summary>
    public string Username { get; set; } = "";

    /// <summary>
    /// Gets or sets the password of the trainer.
    /// </summary>
    public string Password { get; set; } = "";
}
