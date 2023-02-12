using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IPokemonCardAccessor
    {
        List<string> GetAllTagsForPokemonCard(PokemonCard card);
        List<Ability> GetAllAbilitiesForPokemonCard(PokemonCard card);
        List<string> GetAllTypesForPokemonCard(PokemonCard card);
        List<Pokemon> GetAllPokemonForPokemonCard(PokemonCard card);
        List<PokemonCard> GetAllReleasedActivePokemonCards();
        List<PokemonCard> GetAllPokemonCards();
        int UpdatePokemonCard(PokemonCard pokemonCard);
        int InsertPokemonCard(PokemonCard pokemonCard);
    }
}
