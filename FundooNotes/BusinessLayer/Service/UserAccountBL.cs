using BusinessLayer.Interface;
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
            return this.repositoryLayer.AddAccount(userAccount);
        }

        public List<UserAccount> GetAccount()
        {
            return this.repositoryLayer.GetAccount();
        }
    }
}
