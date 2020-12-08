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


        [HttpGet("GetAccount")]
        public IActionResult GetAccount()
        {
            try
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

            }catch(Exception e)
            {
                return this.NotFound(new { sucess = false, message = e.Message});
            }
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

    }
}
