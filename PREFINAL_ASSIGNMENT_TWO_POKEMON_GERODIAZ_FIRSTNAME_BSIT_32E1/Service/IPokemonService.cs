using System.Collections.Generic;
using System.Threading.Tasks;
using PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Models;

namespace PREFINAL_ASSIGNMENT_TWO_POKEMON_GERODIAZ_FIRSTNAME_BSIT_32E1.Services
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> GetAll();
    }
}