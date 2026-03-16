// ┌──────────────────────────────────────────────────────────────┐
// │  AuthService — Gestion de la session utilisateur             │
// │  Stratégie : comptes stockés dans le localStorage            │
// └──────────────────────────────────────────────────────────────┘
using Blazored.LocalStorage;
using PokeBlazor.Models;

namespace PokeBlazor.Services;

/// <summary>
/// Manages user authentication using local storage.
/// </summary>
public class AuthService(ILocalStorageService localStorage)
{
    const string DRESSEURS_KEY = "dresseurs"; // Clé pour la liste des comptes
    const string CURRENT_USER_KEY = "currentUser"; // Clé pour l'utilisateur connecté

    /// <summary>
    /// Registers a new trainer.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns>True if registration is successful, false if the username already exists.</returns>
    public async Task<bool> Register(string username, string password)
    {
        var dresseurs = await localStorage.GetItemAsync<List<Dresseur>>(DRESSEURS_KEY) ?? new();
        if (dresseurs.Any(d => d.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
        {
            return false; // L'utilisateur existe déjà
        }
        dresseurs.Add(new Dresseur { Username = username, Password = password });
        await localStorage.SetItemAsync(DRESSEURS_KEY, dresseurs);
        return true;
    }

    /// <summary>
    /// Logs in the user.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns>True if login is successful, false otherwise.</returns>
    public async Task<bool> Login(string username, string password)
    {
        var dresseurs = await localStorage.GetItemAsync<List<Dresseur>>(DRESSEURS_KEY) ?? new();
        var dresseur = dresseurs.FirstOrDefault(d => d.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && d.Password == password);
        if (dresseur == null)
        {
            return false; // Échec de la connexion
        }
        await localStorage.SetItemAsync(CURRENT_USER_KEY, dresseur.Username);
        return true;
    }

    /// <summary>
    /// Logs out the user.
    /// </summary>
    public async Task Logout()
    {
        await localStorage.RemoveItemAsync(CURRENT_USER_KEY);
    }

    /// <summary>
    /// Checks if a user is authenticated.
    /// </summary>
    /// <returns>True if a user is authenticated, false otherwise.</returns>
    public async Task<bool> IsAuthenticated()
    {
        return await localStorage.GetItemAsync<string>(CURRENT_USER_KEY) != null;
    }

    /// <summary>
    /// Gets the username of the current logged-in user.
    /// </summary>
    /// <returns>The username of the current user, or null if not authenticated.</returns>
    public async Task<string?> GetCurrentUsername()
    {
        return await localStorage.GetItemAsync<string>(CURRENT_USER_KEY);
    }
}
