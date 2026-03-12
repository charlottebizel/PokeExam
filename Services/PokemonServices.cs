// ┌──────────────────────────────────────────────────────────────┐
// │  PokemonService — Appels à PokéAPI (https://pokeapi.co)      │
// │  GetFromJsonAsync<T> : fait le GET + désérialise en une ligne │
// └──────────────────────────────────────────────────────────────┘
using System.Net.Http.Json;
using PokeBlazor.Models;

namespace PokeBlazor.Services;

/// <summary>
/// Service for interacting with the PokéAPI to retrieve Pokémon data.
/// </summary>
public class PokemonService(HttpClient http)
{
    // Dictionnaire français → anglais pour la recherche
    // (PokéAPI n'accepte que les noms anglais dans l'URL)
    static readonly Dictionary<string, string> FrToEn = new()
    {
        ["bulbizarre"] = "bulbasaur",
        ["herbizarre"] = "ivysaur",
        ["florizarre"] = "venusaur",
        ["salamèche"] = "charmander",
        ["reptincel"] = "charmeleon",
        ["dracaufeu"] = "charizard",
        ["carapuce"] = "squirtle",
        ["carabaffe"] = "wartortle",
        ["tortank"] = "blastoise",
        ["chenipan"] = "caterpie",
        ["chrysacier"] = "metapod",
        ["papilusion"] = "butterfree",
        ["aspicot"] = "weedle",
        ["coconfort"] = "kakuna",
        ["dardargnan"] = "beedrill",
        ["roucool"] = "pidgey",
        ["roucoups"] = "pidgeotto",
        ["roucarnage"] = "pidgeot",
        ["rattata"] = "rattata",
        ["rattatac"] = "raticate",
        ["piafabec"] = "spearow",
        ["rapasdepic"] = "fearow",
        ["abo"] = "ekans",
        ["arbok"] = "arbok",
        ["pikachu"] = "pikachu",
        ["raichu"] = "raichu",
        ["sabelette"] = "sandshrew",
        ["sablaireau"] = "sandslash",
        ["nidoran♀"] = "nidoran♀",
        ["nidorina"] = "nidorina",
        ["nidoqueen"] = "nidoqueen",
        ["nidoran♂"] = "nidoran♂",
        ["nidorino"] = "nidorino",
        ["nidoking"] = "nidoking",
        ["mélofée"] = "clefairy",
        ["mélodelfe"] = "clefable",
        ["goupix"] = "vulpix",
        ["feunard"] = "ninetales",
        ["rondoudou"] = "jigglypuff",
        ["grodoudou"] = "wigglytuff",
        ["nosferapti"] = "zubat",
        ["nosferalto"] = "golbat",
        ["mystherbe"] = "oddish",
        ["ortide"] = "gloom",
        ["rafflésia"] = "vileplume",
        ["paras"] = "paras",
        ["parasect"] = "parasect",
        ["mimitoss"] = "venonat",
        ["aéromite"] = "venomoth",
        ["taupiqueur"] = "diglett",
        ["triopikeur"] = "dugtrio",
        ["miaouss"] = "meowth",
        ["persian"] = "persian",
        ["psykokw ak"] = "psyduck",
        ["akwakwak"] = "golduck",
        ["f érosinge"] = "mankey",
        ["colossinge"] = "primeape",
        ["caninos"] = "growlithe",
        ["arcanin"] = "arcanine",
        ["ptitard"] = "poliwag",
        ["têtarte"] = "poliwhirl",
        ["tartard"] = "poliwrath",
        ["abra"] = "abra",
        ["kadabra"] = "kadabra",
        ["alakazam"] = "alakazam",
        ["machoc"] = "machop",
        ["machopeur"] = "machoke",
        ["mackogneur"] = "machamp",
        ["chétiflor"] = "bellsprout",
        ["boustiflor"] = "weepinbell",
        ["empiflor"] = "victreebel",
        ["tentacool"] = "tentacool",
        ["tentacruel"] = "tentacruel",
        ["racaillou"] = "geodude",
        ["gravalanch"] = "graveler",
        ["grolem"] = "golem",
        ["ponyta"] = "ponyta",
        ["galopa"] = "rapidash",
        ["ramoloss"] = "slowpoke",
        ["flagadoss"] = "slowbro",
        ["magnéti"] = "magnemite",
        ["magnéton"] = "magneton",
        ["canarticho"] = "farfetch'd",
        ["doduo"] = "doduo",
        ["dodrio"] = "dodrio",
        ["otaria"] = "seel",
        ["lamantine"] = "dewgong",
        ["tadmorv"] = "grimer",
        ["grotadmorv"] = "muk",
        ["kokiyas"] = "shellder",
        ["crustabri"] = "cloyster",
        ["fantominus"] = "gastly",
        ["spectrum"] = "haunter",
        ["ectoplasma"] = "gengar",
        ["onix"] = "onix",
        ["soporifik"] = "drowzee",
        ["hypnomade"] = "hypno",
        ["krabby"] = "krabby",
        ["krabboss"] = "kingler",
        ["voltorbe"] = "voltorb",
        ["électrode"] = "electrode",
        ["nœunœuf"] = "exeggcute",
        ["noadkoko"] = "exeggutor",
        ["osselait"] = "cubone",
        ["ossatueur"] = "marowak",
        ["kicklee"] = "hitmonlee",
        ["tygnon"] =. "hitmonchan",
        ["excelangue"] = "lickitung",
        ["smogo"] = "koffing",
        ["smogogo"] = "weezing",
        ["rhinocorne"] = "rhyhorn",
        ["rhinoféros"] = "rhydon",
        ["leveinard"] = "chansey",
        ["saquedeneu"] = "tangela",
        ["kangourex"] = "kangaskhan",
        ["hypotrempe"] = "horsea",
        ["hypocéan"] = "seadra",
        ["poissirène"] = "goldeen",
        ["poissoroy"] = "seaking",
        ["stari"] = "staryu",
        ["staross"] = "starmie",
        ["m. mime"] = "mr. mime",
        ["insécateur"] = "scyther",
        ["lippoutou"] = "jynx",
        ["élektek"] = "electabuzz",
        ["magmar"] = "magmar",
        ["scarabrute"] = "pinsir",
        ["tauros"] = "tauros",
        ["magicarpe"] = "magikarp",
        ["léviator"] = "gyarados",
        ["lokhlass"] = "lapras",
        ["métamorph"] = "ditto",
        ["évoli"] = "eevee",
        ["aquali"] = "vaporeon",
        ["voltali"] = "jolteon",
        ["pyroli"] = "flareon",
        ["porygon"] = "porygon",
        ["amonita"] = "omanyte",
        ["amonistar"] = "omastar",
        ["kabuto"] = "kabuto",
        ["kabutops"] = "kabutops",
        ["ptéra"] = "aerodactyl",
        ["ronflex"] = "snorlax",
        ["artikodin"] = "articuno",
        ["électhor"] = "zapdos",
        ["sulfura"] = "moltres",
        ["minidraco"] = "dratini",
        ["draco"] = "dragonair",
        ["dracolosse"] = "dragonite",
        ["mewtwo"] = "mewtwo",
        ["mew"] = "mew",
    };

    /// <summary>
    /// Retrieves Pokémon data from the PokéAPI, including French name and description.
    /// </summary>
    /// <param name="name">The name of the Pokémon (French or English).</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the Pokémon data, or null if not found.</returns>
    public async Task<Pokemon?> GetPokemon(string name)
    {
        // Traduit le nom français en anglais si nécessaire
        name = FrToEn.GetValueOrDefault(name.ToLower(), name.ToLower());

        try
        {
            // Appel 1 : données principales (stats, sprites, types)
            var p = await http.GetFromJsonAsync<Pokemon>($"https://pokeapi.co/api/v2/pokemon/{name}");
            if (p == null) return null;

            // Appel 2 : données d'espèce (via l'ID, plus fiable)
            var species = await http.GetFromJsonAsync<PokemonSpecies>($"https://pokeapi.co/api/v2/pokemon-species/{p.Id}");

            // Remplace le nom anglais par le nom français s'il existe
            var frName = species?.Names.FirstOrDefault(n => n.Language.Name == "fr")?.Name;
            if (frName != null) p.Name = frName;

            // Récupère la description française la plus récente et la nettoie
            var frDesc = species?.FlavorTextEntries
                .LastOrDefault(f => f.Language.Name == "fr")?.FlavorText
                .Replace("\f", " ").Replace("\n", " ");
            if (frDesc != null) p.Description = frDesc;

            return p;
        }
        catch { return null; } // Pokémon introuvable ou erreur réseau → null
    }
}
