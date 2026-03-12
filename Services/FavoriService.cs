// ┌──────────────────────────────────────────────────────────────┐
// │  FavoriService — CRUD complet via localStorage               │
// │  Toutes les données sont stockées sous la clé "favoris"      │
// │  sous forme de List<Favori> sérialisée en JSON               │
// └──────────────────────────────────────────────────────────────┘
using Blazored.LocalStorage;
using PokeBlazor.Models;

namespace PokeBlazor.Services;

/// <summary>
/// Provides CRUD (Create, Read, Update, Delete) operations for favorite Pokémon using local storage.
/// </summary>
public class FavoriService(ILocalStorageService ls)
{
    const string KEY = "favoris";

    /// <summary>
    /// Retrieves the list of all favorite Pokémon from local storage.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of favorites, or an empty list if none are found.</returns>
    public async Task<List<Favori>> GetAll() =>
        await ls.GetItemAsync<List<Favori>>(KEY) ?? new();

    /// <summary>
    /// Adds a new favorite Pokémon to the list with an auto-incremented ID.
    /// </summary>
    /// <param name="f">The favorite Pokémon to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Add(Favori f)
    {
        var list = await GetAll();
        f.Id = list.Count > 0 ? list.Max(x => x.Id) + 1 : 1;
        list.Add(f);
        await ls.SetItemAsync(KEY, list);
    }

    /// <summary>
    /// Updates an existing favorite Pokémon in the list.
    /// </summary>
    /// <param name="f">The favorite Pokémon to update, identified by its ID.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Update(Favori f)
    {
        var list = await GetAll();
        var i = list.FindIndex(x => x.Id == f.Id);
        if (i >= 0) { list[i] = f; await ls.SetItemAsync(KEY, list); }
    }

    /// <summary>
    /// Deletes a favorite Pokémon from the list by its ID.
    /// </summary>
    /// <param name="id">The ID of the favorite Pokémon to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Delete(int id)
    {
        var list = await GetAll();
        list.RemoveAll(x => x.Id == id);
        await ls.SetItemAsync(KEY, list);
    }

    /// <summary>
    /// Checks if a Pokémon is already in the favorites list (case-insensitive).
    /// </summary>
    /// <param name="nom">The name of the Pokémon to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the Pokémon is a favorite; otherwise, false.</returns>
    public async Task<bool> IsFavori(string nom) =>
        (await GetAll()).Any(f => f.NomPokemon.ToLower() == nom.ToLower());
}
