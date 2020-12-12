using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserAccountBL : IUserAccountBL
    {

        public IUserAccountRL repositoryLayer;

        public UserAccountBL(IUserAccountRL repositoryLayer)
        {
            this.repositoryLayer = repositoryLayer;
        }


        public UserAccount AddAccount(UserAccount userAccount)
        {
            try
            {
                return this.repositoryLayer.AddAccount(userAccount);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool DeleteAccount(string id)
        {
            try
            {
                return this.repositoryLayer.DeleteAccount(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ForgetPassword(ForgetPassword model)
        {
           string Token = this.repositoryLayer.ForgetPassword(model);
            return Token;
        }

        public List<UserAccount> GetAccount()
        {
            try
            {
                return this.repositoryLayer.GetAccount();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UserAccountDetails LoginAccount(LoginDetails login)
        {
            return this.repositoryLayer.LoginAccount(login);
        }

        public bool ResetPassword(ResetPassword resetPassword, string accountId)
        {
            bool pass = this.repositoryLayer.ResetPassword(resetPassword, accountId);
            return pass;
        }

        public UserAccount UpdateAccount(UserAccount userAccount, string id)
        {
            try
            {
                return this.repositoryLayer.UpdateAccount(userAccount, id);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
