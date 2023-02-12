using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IPokemonManager
    {
        List<Pokemon> GetAllPokemon();
        void UpdatePokemon(Pokemon pokemon);
        void CreatePokemon(Pokemon pokemon);
    }
}
