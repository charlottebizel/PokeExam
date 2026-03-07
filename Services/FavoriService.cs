// ┌──────────────────────────────────────────────────────────────┐
// │  FavoriService — CRUD complet via localStorage               │
// │  Toutes les données sont stockées sous la clé "favoris"      │
// │  sous forme de List<Favori> sérialisée en JSON               │
// └──────────────────────────────────────────────────────────────┘
using Blazored.LocalStorage;
using PokeBlazor.Models;

namespace PokeBlazor.Services;

public class FavoriService(ILocalStorageService ls)
{
    const string KEY = "favoris";

    // Lit la liste depuis le localStorage (retourne liste vide si absente)
    public async Task<List<Favori>> GetAll() =>
        await ls.GetItemAsync<List<Favori>>(KEY) ?? new();

    // Ajoute un favori avec un Id auto-incrémenté
    public async Task Add(Favori f)
    {
        var list = await GetAll();
        f.Id = list.Count > 0 ? list.Max(x => x.Id) + 1 : 1;
        list.Add(f);
        await ls.SetItemAsync(KEY, list);
    }

    // Met à jour un favori existant (trouvé par son Id)
    public async Task Update(Favori f)
    {
        var list = await GetAll();
        var i = list.FindIndex(x => x.Id == f.Id);
        if (i >= 0) { list[i] = f; await ls.SetItemAsync(KEY, list); }
    }

    // Supprime un favori par son Id
    public async Task Delete(int id)
    {
        var list = await GetAll();
        list.RemoveAll(x => x.Id == id);
        await ls.SetItemAsync(KEY, list);
    }

    // Vérifie si un Pokémon est déjà en favori (comparaison insensible à la casse)
    public async Task<bool> IsFavori(string nom) =>
        (await GetAll()).Any(f => f.NomPokemon.ToLower() == nom.ToLower());
}
