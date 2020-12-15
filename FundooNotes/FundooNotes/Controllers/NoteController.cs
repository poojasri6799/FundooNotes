using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        public INoteBL businessLayer;

        public NoteController(INoteBL businessLayer)
        {
            this.businessLayer = businessLayer;
        }

        [HttpPost("CreateNote")]
        public IActionResult AddNote(NoteModel notemodel)
        {
            try
            {
                string accountID = this.GetAccountId();
                Notes note = this.businessLayer.AddNote(notemodel, accountID);
                if (!note.Equals(null))
                {
                    return this.Ok(new { sucess = true, message = "Note added succesfully", data = note });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Note not added" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { sucess = false, message = e.Message });
            }
        }

        [HttpGet("GetNote")]
        public IActionResult GetNote()
        {
            try
            {
                string accountID = this.GetAccountId();
                var note = businessLayer.GetNote(accountID);
                if (!note.Equals(null))
                {
                    return this.Ok(new { sucess = true, message = "Note displayed succesfully", data = note });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Note is not Exist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { sucess = false, message = e.Message });
            }
        }

        [HttpDelete("{noteId:length(24)}")]
        public IActionResult DeleteNote(string noteId)
        {
            try
            {
                bool note = this.businessLayer.DeleteNote(noteId);
                if (!note.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Note deleted succesfully" });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Note was not deleted" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }


        [HttpPut("{noteId:length(24)}")]
        public IActionResult EditNote(Notes notes, string noteId)
        {
            try
            {
                Notes result = this.businessLayer.EditNotes(notes,noteId);
                if (!result.Equals(false))
                {
                    return this.Ok(new { success = true, Message = "Note is updated successfully", data = result });
                }
                else
                {
                    return this.NotFound(new { success = result, Message = "Note is not updated successfully" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }


        

        [HttpPut("Reminder")]
        public IActionResult AddReminder(Notes reminder,string noteId)
        {
            try
            {
                bool result = businessLayer.AddReminder(reminder, noteId);
                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Reminder set Successfully" });
                }
                else
                    return this.BadRequest(new { success = false, meaasage = " Reminder not set Successfully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }


        [HttpPut("NoteColour")]
        public IActionResult IsColour(Notes colour, string noteId)
        {
            try
            {
                bool result = businessLayer.IsColour(colour, noteId);
                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Colour added Successfully" });
                }
                else
                    return this.BadRequest(new { success = false, meaasage = " Colour not added Successfully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }

        [HttpPut("Image")]
        public IActionResult AddImage(Notes image, string noteId)
        {
            try
            {
                bool result = businessLayer.AddImage(image, noteId);
                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Image added Successfully" });
                }
                else
                    return this.BadRequest(new { success = false, meaasage = "Image not added Successfully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }


        private string GetAccountId()
        {
            return User.FindFirst("Id").Value;
        }
    }
}
