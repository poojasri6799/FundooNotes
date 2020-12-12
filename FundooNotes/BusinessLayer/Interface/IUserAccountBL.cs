using CommonLayer.Model;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserAccountBL
    {

        UserAccount AddAccount(UserAccount userAccount);
        
        List<UserAccount> GetAccount();

        bool DeleteAccount(string id);

        UserAccount UpdateAccount(UserAccount userAccount, string id);

        UserAccountDetails LoginAccount(LoginDetails login);

        string ForgetPassword(ForgetPassword model);

        bool ResetPassword(ResetPassword resetPassword, string accountId);
    }
}
