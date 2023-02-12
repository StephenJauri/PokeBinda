using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;
using DataAccesslayer;

namespace LogicLayer
{
    public class PokemonManager : IPokemonManager
    {
        private IPokemonAccessor _pokemonAccessor = null;
        public PokemonManager()
        {
            _pokemonAccessor = new PokemonAccessor();
        }

        public void CreatePokemon(Pokemon pokemon)
        {
            try
            {
                if (0 ==_pokemonAccessor.InsertPokemon(pokemon))
                {
                    throw new ApplicationException("No records were inserted");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add pokemon", ex);
            }
        }

        public List<Pokemon> GetAllPokemon()
        {
            List<Pokemon> pokemon = null;
            try
            {
                pokemon = _pokemonAccessor.SelectAllPokemon();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load pokemon", ex);
            }
            return pokemon;
        }

        public void UpdatePokemon(Pokemon pokemon)
        {
            try
            {
                if (0==_pokemonAccessor.UpdatePokemon(pokemon))
                {
                    throw new ApplicationException("No records were updated");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update pokemon", ex);
            }
        }
    }
}
