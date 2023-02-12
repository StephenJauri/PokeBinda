using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IPokemonAccessor
    {
        List<Pokemon> SelectAllPokemon();
        int InsertPokemon(Pokemon pokemon);
        int UpdatePokemon(Pokemon pokemon);
    }
}
