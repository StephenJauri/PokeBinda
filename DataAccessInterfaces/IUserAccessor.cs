using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        User SelectUserByEmail(string email);
        int UpdatePasswordHash(int userID, string oldPasswordHash, string newPasswordHash);
        int UpdateAccountInformation(Account oldAccountInfo, Account newAccountInfo);
        int DeactivateAccount(Account account, string passwordHash);
        int InsertAccount(User account, string passwordHash);
        int CheckIfEmailExists(string email);
    }
}
