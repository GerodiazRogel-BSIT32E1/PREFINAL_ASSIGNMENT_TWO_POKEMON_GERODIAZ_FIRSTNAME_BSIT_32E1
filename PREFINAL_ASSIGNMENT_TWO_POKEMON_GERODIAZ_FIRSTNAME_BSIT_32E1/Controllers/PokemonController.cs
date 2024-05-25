using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Models;

namespace PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Controllers
{
    [Route("pokemon")]
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

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _client.GetAsync(
                "https://pokeapi.co/api/v2/pokemon?limit=100"
            );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // Print the responseBody to the console for debugging
            Console.WriteLine(responseBody);

            // Deserialize the responseBody into a PokemonList object
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(responseBody);

            // Print the pokemonList to the console for debugging
            Console.WriteLine(JsonConvert.SerializeObject(pokemonList));

            var model = new HomeViewModel { Pokemons = pokemonList?.Results };

            return View(model);
        }
    }
}
