using Xunit;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PokeBlazor.Services;
using PokeBlazor.Models;
using System.Text.Json;

namespace PokeBlazor.Tests
{
    public class PokemonServiceTests
    {
        private PokemonService _pokemonService;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;

        private void SetupPokemonApiMocks(string pokemonName, string pokemonResponse, string speciesResponse)
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains($"pokemon/{pokemonName}")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(pokemonResponse)
                });

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("pokemon-species/")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(speciesResponse)
                });

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new System.Uri("https://pokeapi.co/api/v2/")
            };
            _pokemonService = new PokemonService(httpClient);
        }

        [Fact]
        public async Task GetPokemon_ShouldReturnPokemon_WhenPokemonExists_EnglishName()
        {
            // Arrange
            var pokemonName = "pikachu";
            var pokemonResponse = @"{""id"": 25, ""name"": ""pikachu""}";
            var speciesResponse = @"{""name"": ""pikachu"", ""names"": [{""language"": {""name"": ""fr""}, ""name"": ""Pikachu""}], ""flavor_text_entries"": [{""language"": {""name"": ""fr""}, ""flavor_text"": ""La description de Pikachu.""}]}";
            SetupPokemonApiMocks(pokemonName, pokemonResponse, speciesResponse);

            // Act
            var result = await _pokemonService.GetPokemon(pokemonName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pikachu", result.Name);
        }

        [Fact]
        public async Task GetPokemon_ShouldReturnPokemon_WhenPokemonExists_FrenchName()
        {
            // Arrange
            var pokemonName = "salamèche";
            var englishName = "charmander";
            var pokemonResponse = @"{""id"": 4, ""name"": ""charmander""}";
            var speciesResponse = @"{""name"": ""charmander"", ""names"": [{""language"": {""name"": ""fr""}, ""name"": ""Salamèche""}], ""flavor_text_entries"": [{""language"": {""name"": ""fr""}, ""flavor_text"": ""La description de Salamèche.""}]}";
            SetupPokemonApiMocks(englishName, pokemonResponse, speciesResponse);

            // Act
            var result = await _pokemonService.GetPokemon(pokemonName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Salamèche", result.Name);
        }

        [Fact]
        public async Task GetPokemon_ShouldReturnNull_WhenPokemonDoesNotExist()
        {
            // Arrange
            var pokemonName = "nonexistentpokemon";
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                });
            var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new System.Uri("https://pokeapi.co/api/v2/")
            };
            _pokemonService = new PokemonService(httpClient);

            // Act
            var result = await _pokemonService.GetPokemon(pokemonName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPokemon_ShouldReturnFrenchName_WhenFrenchNameExists()
        {
            // Arrange
            var pokemonName = "pikachu";
            var pokemonResponse = @"{""id"": 25, ""name"": ""pikachu""}";
            var speciesResponse = @"{""name"": ""pikachu"", ""names"": [{""language"": {""name"": ""fr""}, ""name"": ""Pikachu""}], ""flavor_text_entries"": [{""language"": {""name"": ""fr""}, ""flavor_text"": ""La description de Pikachu.""}]}";
            SetupPokemonApiMocks(pokemonName, pokemonResponse, speciesResponse);

            // Act
            var result = await _pokemonService.GetPokemon(pokemonName);

            // Assert
            Assert.Equal("Pikachu", result.Name);
        }

        [Fact]
        public async Task GetPokemon_ShouldReturnFrenchDescription_WhenFrenchDescriptionExists()
        {
            // Arrange
            var pokemonName = "pikachu";
            var pokemonResponse = @"{""id"": 25, ""name"": ""pikachu""}";
            var speciesResponse = @"{""name"": ""pikachu"", ""names"": [{""language"": {""name"": ""fr""}, ""name"": ""Pikachu""}], ""flavor_text_entries"": [{""language"": {""name"": ""fr""}, ""flavor_text"": ""La description de Pikachu.""}]}";
            SetupPokemonApiMocks(pokemonName, pokemonResponse, speciesResponse);

            // Act
            var result = await _pokemonService.GetPokemon(pokemonName);

            // Assert
            Assert.Equal("La description de Pikachu.", result.Description);
        }
    }
}
