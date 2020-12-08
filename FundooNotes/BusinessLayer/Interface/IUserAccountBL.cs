using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserAccountBL
    {
        //global::RepositoryLayer.UserAccount AddAccount(global::RepositoryLayer.UserAccount userAccount);

        UserAccount AddAccount(UserAccount userAccount);
        
        List<UserAccount> GetAccount();

        UserAccount GetAccountById(string id);
        bool DeleteAccount(string id);
    }
}
