using CommonLayer.Model;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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


        public UserAccountDetails LoginAccount(LoginDetails login)
        {
           List<UserAccount> userValidation = AccountData.Find(user => user.MailId == login.MailId && user.Password == login.Password).ToList();

           UserAccountDetails userAccountDetails = new UserAccountDetails();
           userAccountDetails.Id = userValidation[0].Id;
            userAccountDetails.FirstName = userValidation[0].FirstName;
            userAccountDetails.LastName = userValidation[0].LastName;
            userAccountDetails.MailId = userValidation[0].MailId;
            userAccountDetails.Password = userValidation[0].Password;
            userAccountDetails.Token = CreateToken(login.MailId,userAccountDetails.Id);

            return userAccountDetails;
           
        }

        private string CreateToken(string mailId, string id)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("pooja@jsaifiyfhuuesfy78736586235fnjdh"));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", mailId),

                            new Claim("id",userId),

                        };
            var tokenOptionOne = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
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

