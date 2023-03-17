using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccesslayer;
using DataObjects;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor = null;
        private IUserGroupAccessor _userGroupAccessor = null;
        private IUserPokemonCardAccessor _userPokemonCardAccessor = null;
        private IPokemonCardGroupCardAccessor _pokemonCardGroupCardAccessor = null;
        private ICardManager _cardManager = null;

        public UserManager(ICardManager cardManager)
        {
            _userAccessor = new UserAccessor();
            _userGroupAccessor = new UserGroupAccessor();
            _userPokemonCardAccessor = new UserPokemonCardAccessor();
            _pokemonCardGroupCardAccessor = new PokemonCardGroupAccessor();
            _cardManager = cardManager;
        }

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        public string HashSha256(string source)
        {

            if (source == "" || source == null)
            {
                throw new ArgumentNullException("Missing Input");
            }
            // create a byte array
            byte[] data;

            // create a .NET hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(source));
            }

            // create output with a stringbuilder object
            StringBuilder s = new StringBuilder();

            // loop through the hashed output making characters from the values in the byte array
            Array.ForEach(data, (byteData) => s.Append(byteData.ToString("x2")));

            return s.ToString();
        }
        public User LoginUser(string email, string password)
        {
            User user = null;
            email = email.ToLower();
            try
            {
                password = HashSha256(password);
                if (1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password))
                {
                    user = _userAccessor.SelectUserByEmail(email);
                    LoadUserInformation(user);
                }
                else
                {
                    throw new ApplicationException("Bad Username or Password.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong logging you in.", ex);
            }
            return user;
        }
        public void LoadUserInformation(User user)
        {
            LoadUserGroupInformation(user);
            LoadUserPokemonCardInformation(user);
            AssociateCardsAndGroups(user);
            SeperateFavoriteGroup(user);
        }
        private void AssociateCardsAndGroups(User user)
        {
            try
            {
                List<int> cardInGroup = null;
                foreach (PokemonCardGroupVM group in user.Groups)
                {
                    cardInGroup = _pokemonCardGroupCardAccessor.GetPokemonCardIDsInGroup(group);

                    foreach (UserPokemonCard card in user.PokemonCards)
                    {
                        if (cardInGroup.Contains(card.UserCardID))
                        {
                            group.Cards.Add(card);
                            card.Groups.Add(group);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load group cards", ex);
            }
        }
        private void LoadUserGroupInformation(User user)
        {
            List<PokemonCardGroupVM> groups = null;
            try
            {
                groups = _userGroupAccessor.GetPokemonCardGroupsByUserID(user.ID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Couldn't update group information", ex);
            }
            user.Groups = groups;
        }
        private static void SeperateFavoriteGroup(User user)
        {
            user.FavoriteGroup = user.Groups.First(group => group.Favorite);
            user.Groups.Remove(user.FavoriteGroup);
        }
        private void LoadUserPokemonCardInformation(User user)
        {
            List<UserPokemonCard> userCards = null;
            try
            {
                userCards = _userPokemonCardAccessor.GetAllUserPokemonCards(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Couldn't load cards for user " + user.GivenName + " " + user.FamilyName, ex);
            }
            user.PokemonCards = userCards;
            _cardManager.LoadAllCardsInformation(userCards);

        }
        public void CreateGroup(User user, string groupName)
        {
            PokemonCardGroupVM newGroup = new PokemonCardGroupVM()
            {
                Favorite = false,
                Name = groupName,
                Active = true,
                Cards = new List<UserPokemonCard>()
            };
            try
            {
                newGroup.ID = _userGroupAccessor.CreateUserGroup(user.ID, newGroup);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create the group", ex);
            }
            user.Groups.Add(newGroup);
            user.Groups.Sort((grp, grp2) => grp.Name.ToLower().CompareTo(grp2.Name.ToLower()));
        }
        public void DeleteGroup(User user, PokemonCardGroupVM group)
        {
            try
            {
                _userGroupAccessor.DeleteUserGroup(group.ID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete the group", ex);
            }
            user.Groups.Remove(group);
            foreach (UserPokemonCard card in group.Cards)
            {
                card.Groups.Remove(group);
            }
        }
        public void SetFavoriteGroup(User user, PokemonCardGroupVM group)
        {
            try
            {
                if (0==_userGroupAccessor.ChangeFavoriteGroup(user.ID, group.ID, user.FavoriteGroup.ID))
                {
                    throw new ApplicationException("Favorite group already updated or not found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the favorite group", ex);
            }
            user.FavoriteGroup.Favorite = false;
            group.Favorite = true;
            user.Groups.Add(user.FavoriteGroup);
            user.Groups.Remove(group);
            user.FavoriteGroup = group;

            user.Groups.Sort((grp, grp2) => grp.Name.ToLower().CompareTo(grp2.Name.ToLower()));
        }
        public void ResetPassword(Account user, string password, string newPassword)
        {
            password = HashSha256(password);
            newPassword = HashSha256(newPassword);
            try
            {
                if (0 == _userAccessor.UpdatePasswordHash(user.ID, password, newPassword))
                {
                    throw new ApplicationException("Incorrect password provided");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the password",ex);
            }
        }
        public void RenameGroup(User user, PokemonCardGroupVM group, string newName)
        {
            try
            {
                if (0==_userGroupAccessor.UpdateGroupName(group.ID, group.Name, newName))
                {
                    throw new ApplicationException("Group already updated or not found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the group name", ex);
            }
            group.Name = newName;
            user.Groups.Sort((grp, grp2) => grp.Name.ToLower().CompareTo(grp2.Name.ToLower()));
        }

        public void AddCardToGroup(UserPokemonCard card, PokemonCardGroupVM group, bool newCard = false)
        {
            if (!newCard)
            {
                try
                {
                    if (0 == _pokemonCardGroupCardAccessor.AddCardToGroup(card, group))
                    {
                        throw new ApplicationException("Unable to add card to the selected group");
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to add " + card.Name + " to " + group.Name, ex);
                }
                group.Cards.Add(card);
            }
            card.Groups.Add(group);
        }

        public void RemoveCardFromGroup(UserPokemonCard card, PokemonCardGroupVM group, bool newCard = false)
        {
            if (!newCard)
            {
                try
                {
                    if (0 == _pokemonCardGroupCardAccessor.RemoveCardFromGroup(card, group))
                    {
                        throw new ApplicationException("Unable to remove card from the selected group");
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to remove " + card.Name + " from " + group.Name, ex);
                }
                group.Cards.Remove(card);
            }
            card.Groups.Remove(group);
        }

        public void UpdateUserPokemonCardStatus(UserPokemonCard card, string newStatus, bool newCard)
        {
            if (!newCard)
            {
                try
                {
                    if (0 == _userPokemonCardAccessor.UpdateCardStatus(card, newStatus))
                    {
                        throw new ApplicationException("Status already updated or not found");
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to update the card status", ex);
                }
            }
            card.Status = newStatus;
        }

        public void AddPokemonCardToCollection(UserPokemonCard card, User user)
        {
            try
            {
                card.UserCardID = _userPokemonCardAccessor.CreateUserPokemonCard(card, user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add the card to your collection", ex);
            }
            user.PokemonCards.Add(card);
            try
            {
                card.Groups.ForEach((grp) =>
                {
                    _pokemonCardGroupCardAccessor.AddCardToGroup(card, grp);
                    grp.Cards.Add(card);
            });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add new card to the selected groups", ex);
            }
        }

        public UserPokemonCard CreateUserPokemonCard(PokemonCard card)
        {
            UserPokemonCard newCard = new UserPokemonCard()
            {
                Name = card.Name,
                SetNumber = card.SetNumber,
                Status = "Default",
                Abilities = card.Abilities,
                Active = true,
                DateAdded = DateTime.Now,
                ID = card.ID,
                UserCardActive = true,
                Groups = new List<PokemonCardGroupVM>(),
                HP = card.HP,
                ImageName = card.ImageName,
                Note = card.Note,
                Pokemon = card.Pokemon,
                Released = card.Released,
                ReleaseYear = card.ReleaseYear,
                Tags = card.Tags,
                Types = card.Types
            };
            return newCard;
        }

        public void DeleteUserPokemonCard(UserPokemonCard card, User user)
        {
            try
            {
                _userPokemonCardAccessor.RemoveUserPokemonCard(card);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to remove the card from your collection", ex);
            }
            user.PokemonCards.Remove(card);
            card.Groups.ForEach((grp) => grp.Cards.Remove(card));
        }

        public void ChangeUserInformation(Account oldAccountInfo, Account newAccountInfo)
        {
            try
            {
                if (1 == _userAccessor.UpdateAccountInformation(oldAccountInfo, newAccountInfo))
                {
                    oldAccountInfo.GivenName = newAccountInfo.GivenName;
                    oldAccountInfo.FamilyName = newAccountInfo.FamilyName;
                    oldAccountInfo.Birthday = newAccountInfo.Birthday;
                }
                else
                {
                    throw new ApplicationException("Failed to update information");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to Update account information", ex);
            }
        }

        public void DeleteAccount(Account account, string password)
        {
            password = HashSha256(password);
            try
            {
                if (0 == _userAccessor.DeactivateAccount(account,password))
                {
                    throw new ApplicationException("Password Incorrect");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete account for " + account.Email, ex);
            }
        }

        public void CreateUserAccount(User account, string password)
        {
            account.Email = account.Email.ToLower();
            password = HashSha256(password);
            try
            {
                if (0 != _userAccessor.CheckIfEmailExists(account.Email))
                {
                    throw new ApplicationException("An account with that email already exists");
                }
                if (0 == _userAccessor.InsertAccount(account, password))
                {
                    throw new ApplicationException("No account created");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create account", ex);
            }
        }
    }
}
