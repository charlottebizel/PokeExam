// ┌──────────────────────────────────────────────────────────────┐
// │  AuthService — Gestion de la session utilisateur             │
// │  Stratégie simple : un booléen en localStorage               │
// │  (pas de JWT, pas de serveur
// └──────────────────────────────────────────────────────────────┘
using Blazored.LocalStorage;

namespace PokeBlazor.Services;

/// <summary>
/// Manages user authentication using local storage.
/// </summary>
public class AuthService(ILocalStorageService localStorage)
{
    const string KEY = "isAuthenticated"; // clé utilisée dans le localStorage

    /// <summary>
    /// Logs in the user if the username and password are not empty.
    /// </summary>
    /// <param name="u">The username.</param>
    /// <param name="p">The password.</param>
    /// <returns>A task that represents the asynchronous login operation.</returns>
    public Task Login(string u, string p) =>
        (!string.IsNullOrWhiteSpace(u) && !string.IsNullOrWhiteSpace(p))
            ? localStorage.SetItemAsync(KEY, true).AsTask()
            : Task.CompletedTask;

    /// <summary>
    /// Logs out the user by removing the authentication key from local storage.
    /// </summary>
    /// <returns>A task that represents the asynchronous logout operation.</returns>
    public Task Logout() => localStorage.RemoveItemAsync(KEY).AsTask();

    /// <summary>
    /// Checks if the user is authenticated.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the user is authenticated; otherwise, false.</returns>
    public Task<bool> IsAuthenticated() => localStorage.GetItemAsync<bool>(KEY).AsTask();
}
