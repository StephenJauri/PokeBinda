using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        List<User> fakeUsers = null;
        List<string> fakePasswords = null;
        public UserAccessorFake()
        {
            fakeUsers = new List<User>()
            {
                new User()
                {
                    ID=20,
                    GivenName="Stefo",
                    FamilyName="Jarig",
                    Email="goofy@gmail.com",
                    Active=true,
                    CreationDate=DateTime.Now,
                    Birthday=DateTime.Parse("2000-04-23"),
                    Groups = new List<PokemonCardGroupVM>()
                    {
                        new PokemonCardGroupVM()
                        {
                            ID = 3,
                            Active = true,
                            Favorite = true,
                            Name = "Favorite"
                        }
                    }
                },
                new User()
                {
                    ID=20,
                    GivenName="Feind",
                    FamilyName="Wierd",
                    Email="boby@gmail.com",
                    Active=true,
                    CreationDate=DateTime.Now,
                    Birthday=DateTime.Parse("1990-04-23"),
                    Groups = new List<PokemonCardGroupVM>()
                    {
                        new PokemonCardGroupVM()
                        {
                            ID = 1,
                            Active = true,
                            Favorite = true,
                            Name = "Favorite"
                        },
                        new PokemonCardGroupVM()
                        {
                            ID = 2,
                            Active = true,
                            Favorite = true,
                            Name = "Custom 1"
                        }
                    }
                }
            };
            fakePasswords = new List<string>()
            {
                "ecef7b1e64c70decb9786df778d470f7288c02eeb6b95c97dade5b46d768ab50",
                "3693d93220b28a03d3c70bdc1cab2b890c65a2e6baff3d4a2a651b713c161c5c"
            };
        }
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].Email == email && fakePasswords[i] == passwordHash)
                {
                    return 1;
                }
            }
            return 0;
        }

        public int ChangeFavoriteGroup(int userID, int groupID, int currentFavorite)
        {
            throw new NotImplementedException();
        }

        public int CheckIfEmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public int CreateUserGroup(int userID, string groupName)
        {
            throw new NotImplementedException();
        }

        public int DeactivateAccount(Account account, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserGroup(int groupID)
        {
            throw new NotImplementedException();
        }

        public List<PokemonCardGroupVM> GetPokemonCardGroupsByUserID(int userID)
        {
            foreach(User user in fakeUsers)
            {
                if (user.ID == userID)
                {
                    return user.Groups;
                }
            }
            return new List<PokemonCardGroupVM>();
        }

        public int InsertAccount(User account, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public User SelectUserByEmail(string email)
        {
            foreach (User user in fakeUsers)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            throw new ApplicationException("No such user exists");
        }

        public int UpdateAccountInformation(Account oldAccountInfo, Account newAccountInfo)
        {
            throw new NotImplementedException();
        }

        public int UpdateGroupName(int groupID, string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(int userID, string oldPasswordHash, string newPasswordHash)
        {
            throw new NotImplementedException();
        }
    }
}
