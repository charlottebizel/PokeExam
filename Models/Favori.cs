// Modèle d'un favori sauvegardé en localStorage
namespace PokeBlazor.Models;

public class Favori
{
    public int     Id          { get; set; }  // Identifiant unique auto-incrémenté
    public string  NomPokemon  { get; set; } = "";
    public string? Commentaire { get; set; }  // Nullable : peut ne pas avoir de commentaire
    public string? ImageUrl    { get; set; }  // URL du sprite pour l'affichage
}
