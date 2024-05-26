using System.Collections.Generic;
using System.Threading.Tasks;
using PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Models;

namespace PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Services
{
    public class PokemonService : IPokemonService
    {
        public async Task<IEnumerable<Pokemon>> GetAll()
        {
            // This is just a placeholder. Replace this with your actual implementation.
            return await Task.FromResult(new List<Pokemon>());
        }

        // Implement other methods as needed...
    }
}
