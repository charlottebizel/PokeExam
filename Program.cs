// ┌─────────────────────────────────────────┐
// │  Point d'entrée de l'application Blazor │
// └─────────────────────────────────────────┘
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PokeBlazor;
using PokeBlazor.Services;
using Blazored.LocalStorage;

try
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);

    // Composants racine : #app = zone d'affichage, HeadOutlet = balises <head>
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");

    // HttpClient configuré avec l'URL de base (nécessaire pour appeler PokéAPI)
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

    // LocalStorage : permet de sauvegarder données dans le navigateur
    builder.Services.AddBlazoredLocalStorage();

    // Nos services métier (injection de dépendances)
    builder.Services.AddScoped<AuthService>();
    builder.Services.AddScoped<PokemonService>();
    builder.Services.AddScoped<FavoriService>();

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred during startup: {ex}");
}
