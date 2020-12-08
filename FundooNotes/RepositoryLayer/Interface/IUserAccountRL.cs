using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserAccountRL
    {
        UserAccount AddAccount(UserAccount userAccount);

        List<UserAccount> GetAccount();

        UserAccount GetAccountById(string id);
        bool DeleteAccount(string id);
    }
}
