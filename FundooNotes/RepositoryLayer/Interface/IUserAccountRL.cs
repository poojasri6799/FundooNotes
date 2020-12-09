using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserAccountRL
    {
        UserAccount AddAccount(UserAccount userAccount);

        List<UserAccount> GetAccount();

        bool DeleteAccount(string id);

        UserAccount UpdateAccount(UserAccount userAccount, string id);

        UserAccountDetails LoginAccount(LoginDetails login);
    }
}
