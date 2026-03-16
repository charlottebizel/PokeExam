// ┌──────────────────────────────────────────────────────────────┐
// │  FavoriService — CRUD complet via localStorage               │
// │  Les favoris sont maintenant liés à l'utilisateur connecté   │
// └──────────────────────────────────────────────────────────────┘
using Blazored.LocalStorage;
using PokeBlazor.Models;

namespace PokeBlazor.Services;

/// <summary>
/// Provides CRUD operations for user-specific favorite Pokémon using local storage.
/// </summary>
public class FavoriService(ILocalStorageService ls, AuthService authService)
{
    private async Task<string> GetKey()
    {
        var username = await authService.GetCurrentUsername();
        if (string.IsNullOrEmpty(username))
        {
            // Should not happen if pages are protected, but as a fallback
            return "favoris_anonymous"; 
        }
        return $"favoris_{username}";
    }

    /// <summary>
    /// Retrieves the list of all favorite Pokémon for the current user.
    /// </summary>
    /// <returns>A list of favorites, or an empty list if none are found.</returns>
    public async Task<List<Favori>> GetAll()
    {
        var key = await GetKey();
        return await ls.GetItemAsync<List<Favori>>(key) ?? new();
    }

    /// <summary>
    /// Adds a new favorite Pokémon to the current user's list.
    /// </summary>
    public async Task Add(Favori f)
    {
        var key = await GetKey();
        var list = await GetAll();
        f.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        list.Add(f);
        await ls.SetItemAsync(key, list);
    }

    /// <summary>
    /// Updates an existing favorite Pokémon in the current user's list.
    /// </summary>
    public async Task Update(Favori f)
    {
        var key = await GetKey();
        var list = await GetAll();
        var i = list.FindIndex(x => x.Id == f.Id);
        if (i >= 0) { list[i] = f; await ls.SetItemAsync(key, list); }
    }

    /// <summary>
    /// Deletes a favorite Pokémon from the current user's list by its ID.
    /// </summary>
    public async Task Delete(int id)
    {
        var key = await GetKey();
        var list = await GetAll();
        list.RemoveAll(x => x.Id == id);
        await ls.SetItemAsync(key, list);
    }

    /// <summary>
    /// Checks if a Pokémon is in the current user's favorites.
    /// </summary>
    public async Task<bool> IsFavori(string nom) =>
        (await GetAll()).Any(f => f.NomPokemon.Equals(nom, StringComparison.OrdinalIgnoreCase));
}
