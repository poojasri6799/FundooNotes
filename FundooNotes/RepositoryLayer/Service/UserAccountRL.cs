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
            this.AccountData.InsertOne(userAccount);
            return userAccount;
        }

        public bool DeleteAccount(string id)
        {
            this.AccountData.DeleteOne(userAccount => userAccount.Id == id);
            return true;
        }

        public List<UserAccount> GetAccount()
        {
            return this.AccountData.Find(UserAccount => true).ToList();
        }

        public UserAccount GetAccountById(string id)
        {
            return this.AccountData.Find(userAccount => userAccount.Id == id).FirstOrDefault();
        }
    }
}

/*public UserAccount AddAccount(UserAccount userAccount, string mailid)
{ 
    this.AccountData.InsertOne(userAccount);
    return userAccount;

}*/