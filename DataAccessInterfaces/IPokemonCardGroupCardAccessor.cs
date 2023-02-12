using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IPokemonCardGroupCardAccessor
    {
        List<int> GetPokemonCardIDsInGroup(PokemonCardGroup group);
        int AddCardToGroup(UserPokemonCard card, PokemonCardGroup group);
        int RemoveCardFromGroup(UserPokemonCard card, PokemonCardGroup group);
    }
}
