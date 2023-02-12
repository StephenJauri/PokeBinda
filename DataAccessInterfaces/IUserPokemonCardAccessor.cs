using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IUserPokemonCardAccessor
    {
        List<UserPokemonCard> GetAllUserPokemonCards(User user);
        int UpdateCardStatus(UserPokemonCard card, string newStatus);
        int CreateUserPokemonCard(UserPokemonCard card, User user);
        int RemoveUserPokemonCard(UserPokemonCard card);
    }
}
