using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Models;

namespace PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Controllers
{
    [Route("")]
    public class PokemonController : Controller
    {
        private readonly HttpClient _client;

        public PokemonController()
        {
            _client = new HttpClient();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemonDetails(int id)
        {
            HttpResponseMessage response = await _client.GetAsync(
                $"https://pokeapi.co/api/v2/pokemon/{id}"
            );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the responseBody into a Pokemon object
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(responseBody);

            return Ok(pokemon);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPokemonList()
        {
            HttpResponseMessage response = await _client.GetAsync(
                "https://pokeapi.co/api/v2/pokemon?limit=100"
            );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the responseBody into a PokemonList object
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(responseBody);

            // Return the view with the list of Pokemon
            return View("Index", pokemonList?.Results);
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _client.GetAsync(
                "https://pokeapi.co/api/v2/pokemon?limit=100"
            );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the responseBody into a PokemonList object
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(responseBody);

            // Create a new list to store the Pokemon with their ImageUrl
            var pokemonsWithImageUrl = new List<Pokemon>();

            // Iterate over the Results and get the ImageUrl of each Pokemon
            foreach (var pokemon in pokemonList?.Results)
            {
                // Make a GET request to the url of the Pokemon
                HttpResponseMessage pokemonResponse = await _client.GetAsync(pokemon.Url);
                pokemonResponse.EnsureSuccessStatusCode();
                string pokemonResponseBody = await pokemonResponse.Content.ReadAsStringAsync();

                // Deserialize the pokemonResponseBody into a Pokemon object
                var pokemonDetails = JsonConvert.DeserializeObject<PokemonDetails>(
                    pokemonResponseBody
                );

                // Create a new Pokemon object and set the ImageUrl
                var pokemonWithImageUrl = new Pokemon
                {
                    Name = pokemon.Name,
                    ImageUrl = pokemonDetails.Sprites.FrontDefault
                };

                // Add the Pokemon to the list
                pokemonsWithImageUrl.Add(pokemonWithImageUrl);
            }

            var model = new HomeViewModel { Pokemons = pokemonsWithImageUrl };

            return View(model);
        }
    }
}
