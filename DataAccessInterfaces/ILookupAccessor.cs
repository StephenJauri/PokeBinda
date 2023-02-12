using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ILookupAccessor
    {
        List<string> SelectAllCardTypes();
        List<Pokemon> SelectAllActiveReleasedPokemon();
        List<string> SelectAllTags();
        List<int> SelectAllPokemonGens();
        List<string> SelectAllStatuses();
    }
}
