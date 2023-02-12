using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IUserGroupAccessor
    {
        List<PokemonCardGroupVM> GetPokemonCardGroupsByUserID(int userID);
        int CreateUserGroup(int userID, PokemonCardGroup group);
        int DeleteUserGroup(int groupID);
        int ChangeFavoriteGroup(int userID, int groupID, int currentFavorite);
        int UpdateGroupName(int groupID, string oldName, string newName);

    }
}
