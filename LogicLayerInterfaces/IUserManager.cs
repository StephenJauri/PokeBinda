using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        // this method should return a user or throw a bad argument exception
        User LoginUser(string email, string password);
        void CreateGroup(User user, string groupName);
        void DeleteGroup(User user, PokemonCardGroupVM group);
        void SetFavoriteGroup(User user, PokemonCardGroupVM group);
        string HashSha256(string source);
        void ResetPassword(Account user, string password, string newPassword);
        void RenameGroup(User user, PokemonCardGroupVM group, string newName);
        void ChangeUserInformation(Account oldAccountInfo, Account newAccountInfo);
        void DeleteAccount(Account account, string password);
        void AddCardToGroup(UserPokemonCard card, PokemonCardGroupVM group, bool newCard);
        void RemoveCardFromGroup(UserPokemonCard card, PokemonCardGroupVM group, bool newCard);
        void UpdateUserPokemonCardStatus(UserPokemonCard card, string newStatus, bool newCard);
        void AddPokemonCardToCollection(UserPokemonCard card, User user);
        UserPokemonCard CreateUserPokemonCard(PokemonCard card);
        void DeleteUserPokemonCard(UserPokemonCard card, User user);
        void CreateUserAccount(User account, string password);
        bool FindUser(string user);
        int RetrieveUserIDFromEmail(string email);
        User LoginUser(string email);
    }
}
