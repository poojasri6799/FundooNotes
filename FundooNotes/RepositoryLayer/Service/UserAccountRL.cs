using CommonLayer.Model;
using CommonLayer.MSMQ;
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


        public bool AddAccount(UserAccount userAccount)
        {
            int count = 0;
            UserAccount user = new UserAccount()
            {
                FirstName = userAccount.FirstName,
                LastName = userAccount.LastName,
                MailId = userAccount.MailId,
                Password = userAccount.Password
            };

            List<UserAccount> list = AccountData.Find(userAccount => true).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].MailId.Equals(user.MailId))
                    count++;
            }
            if (count == 0)
            {
                AccountData.InsertOne(user);
                return true;
            }
            return false;
        }

        

        public UserAccountDetails LoginAccount(LoginDetails login)
        {
            try
            {
                string pass = EncryptPassword(login.Password);
                List<UserAccount> userValidation = AccountData.Find(user => user.MailId == login.MailId && user.Password == login.Password).ToList();

                UserAccountDetails userAccountDetails = new UserAccountDetails();
                userAccountDetails.Id = userValidation[0].Id;
                userAccountDetails.FirstName = userValidation[0].FirstName;
                userAccountDetails.LastName = userValidation[0].LastName;
                userAccountDetails.MailId = userValidation[0].MailId;
                userAccountDetails.Password = pass;
                userAccountDetails.Token = CreateToken(login.MailId, userAccountDetails.Id);
                return userAccountDetails;
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

        

        public string ForgetPassword(ForgetPassword model)
        {
            try
            {
                List<UserAccount> validation = AccountData.Find(user => user.MailId == model.MailId).ToList();
                UserAccount result = new UserAccount();
                result.MailId = validation[0].MailId;
                result.Id = validation[0].Id;
                string Token = CreateToken(result.MailId, result.Id);
                MsmqSample msmq = new MsmqSample();
                msmq.SendToMsmq(Token, result.Id);
                return Token;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ResetPassword(ResetPassword resetPassword, string accountId)
        {
            try
            {
                var filter = Builders<UserAccount>.Filter.Eq("Id", accountId);
                var update = Builders<UserAccount>.Update.Set("Password", resetPassword.newPassword);
                AccountData.UpdateOne(filter, update);
                return true;

            }catch(Exception e)
            {
                throw e;
            }
        }

        private static string EncryptPassword(string Password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            byte[] encrypt = provider.ComputeHash(encoding.GetBytes(Password));
            String encrypted = Convert.ToBase64String(encrypt);
            return encrypted;
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
    }






    /*public UserAccountDetails LoginAccount(LoginDetails login)
        {
            try
            {
                int count = 0;
                UserAccount user = new UserAccount()
                {
                    MailId = login.MailId,
                    Password = login.Password
                };

                List<UserAccount> list = AccountData.Find(userAccount => true).ToList();

                for(int i=0; i< list.Count; i++)
                {
                    if (list[i].MailId.Equals(user.MailId)&& list[i].Password.Equals(user.Password))
                    {
                        string pass = EncryptPassword(login.Password);
                        //List<UserAccount> userValidation = AccountData.Find(user => user.MailId == login.MailId && user.Password == login.Password).ToList();

                        UserAccountDetails userAccountDetails = new UserAccountDetails();
                        userAccountDetails.Id = list[0].Id;
                        userAccountDetails.FirstName = list[0].FirstName;
                        userAccountDetails.LastName = list[0].LastName;
                        userAccountDetails.MailId = list[0].MailId;
                        userAccountDetails.Password = pass;
                        userAccountDetails.Token = CreateToken(login.MailId, userAccountDetails.Id);
                        count++;
                        return userAccountDetails;
                    }
                }
                if(count == 0)
                {
                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/
}

