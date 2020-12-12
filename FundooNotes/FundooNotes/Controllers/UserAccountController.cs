﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        public IUserAccountBL businessLayer;

        public UserAccountController(IUserAccountBL businessLayer)
        {
            this.businessLayer = businessLayer;
        }


        [AllowAnonymous]
        [HttpPost("LoginAccount")]
        public IActionResult LoginAccount(LoginDetails login)
        {
             UserAccountDetails result = this.businessLayer.LoginAccount(login); 
             if(!result.Equals(null))
             {
                return this.Ok(new { sucess = true, message = "User login succesfully", data = result });
              }
              else
              {
                return this.NotFound(new { sucess = false, message = "Invalid details" });
              }
        }


        
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPassword model)
        {
            string Token = this.businessLayer.ForgetPassword(model);

            string body = "http://localhost:44399/resetPassword/" + Token;

            SendMail(model.MailId, model.MailId, body);

            return this.Ok(new { sucess = true, message = "Reset password link has been sent to email"});
        }


        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword resetPassword)
        {
            if(resetPassword != null)
            {
                var accountId = this.GetAccountId();
                this.businessLayer.ResetPassword(resetPassword, accountId);
                return this.Ok(new { sucess = true, message = "Password is changed successfully"}); 
            }
            else
            {
               return this.NotFound(new { sucess = false, message = "password cann't be empty" });
            }
        }

        
        

        [HttpGet("GetAccount")]
        public IActionResult GetAccount()
        {
            try
            {
                var userDetails = businessLayer.GetAccount();
                if (!userDetails.Equals(null))
                {
                    return this.Ok(new { sucess = true, message = "User Account read succesfully", data = userDetails });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Account not Exist" });
                }

            }catch(Exception e)
            {
                return this.NotFound(new { sucess = false, message = e.Message});
            }
        }


        private string GetAccountId()
        {
            return User.FindFirst("Id").Value;
        }


        [HttpPost("CreateAccount")]
        public IActionResult AddAccount(UserAccount userAccount)
        {
            try
            {
                UserAccount userDetails = this.businessLayer.AddAccount(userAccount);
                if (!userDetails.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "User Account added succesfully", data = userDetails });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Account not added" });
                }
            }
            catch(Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteAccount(string id)
        {
            try
            {
                bool userDetails = this.businessLayer.DeleteAccount(id);
                if (!userDetails.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "User Account deleted succesfully" });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Account not deleted" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }

        
        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateAccount(UserAccount userAccount,string id)
        {
            try
            {
                UserAccount userDetails = this.businessLayer.UpdateAccount(userAccount,id);
                if (!userDetails.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "User Account updated succesfully", data = userDetails });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Account not updated" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }

        }
        
        private static void SendMail(string from, string to, string body)
        {
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = "reset password";
            mailMessage.Body = body;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential()
            {
                UserName = from,
                Password = "poojaec97"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
