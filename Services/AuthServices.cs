// ┌──────────────────────────────────────────────────────────────┐
// │  AuthService — Gestion de la session utilisateur             │
// │  Stratégie simple : un booléen en localStorage               │
// │  (pas de JWT, pas de serveur
// └──────────────────────────────────────────────────────────────┘
using Blazored.LocalStorage;

namespace PokeBlazor.Services;

public class AuthService(ILocalStorageService localStorage)
{
    const string KEY = "isAuthenticated"; // clé utilisée dans le localStorage

    // Connecte l'utilisateur si les champs ne sont pas vides
    public Task Login(string u, string p) =>
        (!string.IsNullOrWhiteSpace(u) && !string.IsNullOrWhiteSpace(p))
            ? localStorage.SetItemAsync(KEY, true).AsTask()
            : Task.CompletedTask;

    // Déconnexion : supprime la clé du localStorage
    public Task Logout() => localStorage.RemoveItemAsync(KEY).AsTask();

    // Vérifie si l'utilisateur est connecté
    public Task<bool> IsAuthenticated() => localStorage.GetItemAsync<bool>(KEY).AsTask();
}
