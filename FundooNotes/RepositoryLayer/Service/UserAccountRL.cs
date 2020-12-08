using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserAccountRL : IUserAccountRL
    {
        private readonly IMongoCollection<UserAccount> AccountData;

        public UserAccountRL(IFundooNotesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            this.AccountData = database.GetCollection<UserAccount>(settings.FundooCollectionName);
        }

        public UserAccount AddAccount(UserAccount userAccount)
        {
            try
            {
                this.AccountData.InsertOne(userAccount);
                return userAccount;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool DeleteAccount(string id)
        {
            try
            {
                this.AccountData.DeleteOne(userAccount => userAccount.Id == id);
                return true;
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
                return this.AccountData.Find(UserAccount => true).ToList();
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
                userAccount.Id = id;
                this.AccountData.ReplaceOne(userAccount => userAccount.Id == id, userAccount);
                return userAccount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

