﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.NoteModels;
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

        [HttpPost]
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

        [HttpGet]
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

        [HttpPut("Archive/{noteId}")]
        public IActionResult IsArchive(string noteId)
        {
            try
            {

                var result = this.businessLayer.IsArchive(noteId);
                if (result == true)
                    return this.Ok(new { sucess = true, message = "Notes Archive Successfully" });
                else
                    return this.BadRequest(new { success = false, meaasage = " Notes doesn't Archive" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }

        [HttpGet("Archive")]
        public IActionResult GetArchive()
        {
            try
            {
                List<Notes> result = this.businessLayer.GetArchive();
                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Archive notes read succesfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Archive was not displayed" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { sucess = false, message = e.Message });
            }
        }

        [HttpGet("Archive")]
        public IActionResult GetTrash()
        {
            try
            {
                List<Notes> result = this.businessLayer.GetTrash();
                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Trash notes read succesfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Trash notes was not displayed" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { sucess = false, message = e.Message });
            }
        }

        [HttpPut("Trash/{noteId}")]
        public IActionResult IsTrash(string noteId)
        {
            try
            {

                var result = this.businessLayer.IsTrash(noteId);
                if (result == true)
                    return this.Ok(new { sucess = true, message = "Notes Trashed Successfully" });
                else
                    return this.BadRequest(new { success = false, meaasage = " Notes doesn't add to Trash" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }

        [HttpPut("Pin/{noteId}")]
        public IActionResult IsPin(string noteId)
        {
            try
            {

                var result = this.businessLayer.IsPin(noteId);
                if (result == true)
                    return this.Ok(new { sucess = true, message = "Notes Pinned Successfully" });
                else
                    return this.BadRequest(new { success = false, meaasage = " Notes was not Pinned" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }

        [HttpPut("Colour/{noteId}")]
        public IActionResult IsColour(Colour colour, string noteId)
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

        [HttpPut("Reminder/{noteId}")]
        public IActionResult AddReminder(NoteReminder reminder,string noteId)
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


        

        [HttpPut("Image/{noteId}")]
        public IActionResult AddImage(NoteImage image, string noteId)
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
