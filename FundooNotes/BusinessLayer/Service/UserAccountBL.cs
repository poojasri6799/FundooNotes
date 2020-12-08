﻿using BusinessLayer.Interface;
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
