using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Models;
using PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Services;

namespace PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Controllers
{
    [Route("")]
    public class PokemonController : Controller
    {
        private readonly HttpClient _client;
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
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
        [Route("Index/{page?}")]
        public async Task<IActionResult> Index(int page = 1)
        {
            const int itemsPerPage = 8;
            int offset = (page - 1) * itemsPerPage;

            HttpResponseMessage response = await _client.GetAsync(
                $"https://pokeapi.co/api/v2/pokemon?limit={itemsPerPage}&offset={offset}"
            );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(responseBody);

            int totalPages = (int)Math.Ceiling((double)(pokemonList?.Count ?? 0) / itemsPerPage);

            var pokemonsWithImageUrl = new List<Pokemon>();

            foreach (var pokemon in pokemonList?.Results ?? Enumerable.Empty<Pokemon>())
            {
                HttpResponseMessage pokemonResponse = await _client.GetAsync(pokemon.Url);
                pokemonResponse.EnsureSuccessStatusCode();
                string pokemonResponseBody = await pokemonResponse.Content.ReadAsStringAsync();

                var pokemonDetails = JsonConvert.DeserializeObject<PokemonDetails>(
                    pokemonResponseBody
                );

                var pokemonWithImageUrl = new Pokemon
                {
                    Name = pokemon.Name,
                    ImageUrl = pokemonDetails?.Sprites?.FrontDefault
                };

                pokemonsWithImageUrl.Add(pokemonWithImageUrl);
            }

            var model = new HomeViewModel
            {
                Pokemons = pokemonsWithImageUrl,
                TotalCount = pokemonList?.Count ?? 0
            };

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages; // Pass the total number of pages to the view

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(model);
            }

            return View(model);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Pokemon>> GetAll()
        {
            HttpResponseMessage response = await _client.GetAsync(
                "https://pokeapi.co/api/v2/pokemon?limit=1000"
            );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the responseBody into a PokemonList object
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(responseBody);

            return pokemonList?.Results ?? new List<Pokemon>();
        }
    }
}
