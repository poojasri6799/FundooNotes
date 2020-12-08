using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
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


        [HttpGet]
        public IActionResult GetAccount()
        {
            var userDetails = businessLayer.GetAccount();
            if (!userDetails.Equals(null))
            {
                return this.Ok(new { sucess = true, message = "User Account added succesfully", data = userDetails });
            }
            else
            {
                return this.NotFound(new { sucess = false, message = "Account not Exist" });
            }
        }

        
        [HttpPost]
        public IActionResult AddAccount(UserAccount userAccount)
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


        [HttpDelete("{id:length(24)}")]

        public IActionResult DeleteAccount(string id)
        {
            bool userDetails = this.businessLayer.DeleteAccount(id);
            if (!userDetails.Equals(false))
            {
                return this.Ok(new { sucess = true, message = "User Account deleted succesfully"});
            }
            else
            {
                return this.NotFound(new { sucess = false, message = "Account not deleted" });
            }
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [HttpGet("{id:length(24)}")]

        public IActionResult GetAccountById(string id)
        {
            UserAccount empDetails = this.businessLayer.GetAccountById(id);

            return this.Ok(new { sucess = true, message = "Accout details are read succesfully by Id", data = empDetails });
        }
    }
}
