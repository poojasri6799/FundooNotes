using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using CommonLayer.NoteModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {

        private readonly IMongoCollection<Notes> Note;

        public NoteRL(IFundooNotesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            this.Note = database.GetCollection<Notes>(settings.NoteCollectionName);
        }


        public Notes AddNote(NoteModel notemodel, string accountID)
        {
            try
            {
                Notes note = new Notes()
                {
                    Title = notemodel.Title,
                    AccountId = accountID,
                    Message = notemodel.Message,
                    Image = notemodel.Image,
                    Color = notemodel.Color,
                    AddReminder = notemodel.AddReminder,
                    Collabration = notemodel.Collabration,
                    IsNote = notemodel.IsNote
                };
                this.Note.InsertOne(note);
                return note;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool AddReminder(NoteReminder reminder, string noteId)
        {
            try
            {
                List<Notes> list = this.Note.Find(notes => notes.NoteId == noteId).ToList();

                var NoteId = Builders<Notes>.Filter.Eq("NoteId", noteId);
                var AddReminder = Builders<Notes>.Update.Set("AddReminder", reminder.AddReminder.ToLocalTime());
                this.Note.UpdateOne(NoteId, AddReminder);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsColour(Colour colour, string noteId)
        {
            try
            {
                List<Notes> list = this.Note.Find(notes => notes.NoteId == noteId).ToList();

                var NoteId = Builders<Notes>.Filter.Eq("NoteId", noteId);
                var Color = Builders<Notes>.Update.Set("Color", colour.Color);
                this.Note.UpdateOne(NoteId, Color);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteNote(string noteId)
        {
            try
            {
                this.Note.DeleteOne(note => note.NoteId == noteId);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Notes EditNotes(Notes notes, string noteId)
        {
            try
            {
                this.Note.ReplaceOne(note => note.NoteId == noteId, notes);
                return notes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> GetNote(string accountID)
        {
            try
            {
                return this.Note.Find(note => note.AccountId == accountID).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> SearchNote(string search)
        {
            try
            {
                List <Notes> list = this.Note.Find(note => note.IsTrash == false && (note.Title.Contains(search) || note.Message.Contains(search) || note.Collabration.Contains(search))).ToList();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> SearchCollabrator()
        {
            try
            {
                return Note.Find(note => note.Collabration != "string").ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> GetArchive()
        {
            try
            {
                return this.Note.Find(note => note.IsArchive == true).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> GetTrash()
        {
            try
            {
                return this.Note.Find(note => note.IsTrash == true).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public bool IsArchive(string id)
        {
            List<Notes> list = this.Note.Find(notes => notes.NoteId == id).ToList();
            if(list[0].IsArchive == true)
            {
                var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                var IsArchive = Builders<Notes>.Update.Set("IsArchive", false);
                var IsNote = Builders<Notes>.Update.Set("IsNote", true);
                var IsTrash = Builders<Notes>.Update.Set("IsTrash", false);
                Note.UpdateOne(NoteId, IsArchive);
                Note.UpdateOne(NoteId, IsNote);
                Note.UpdateOne(NoteId, IsTrash);
                return true;
            }
            else
            {
                var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                var IsArchive = Builders<Notes>.Update.Set("IsArchive", true);
                var IsNote = Builders<Notes>.Update.Set("IsNote", false);
                var IsTrash = Builders<Notes>.Update.Set("IsTrash", false);
                Note.UpdateOne(NoteId, IsArchive);
                Note.UpdateOne(NoteId, IsNote);
                Note.UpdateOne(NoteId, IsTrash);
                return true;
            }
        }

        public bool IsTrash(string id)
        {
            try
            {
                List<Notes> list = this.Note.Find(notes => notes.NoteId == id).ToList();

                if (list[0].IsTrash == true)
                {
                    var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                    var IsArchive = Builders<Notes>.Update.Set("IsArchive", false);
                    var IsNote = Builders<Notes>.Update.Set("IsNote", true);
                    var IsTrash = Builders<Notes>.Update.Set("IsTrash", false);
                    Note.UpdateOne(NoteId, IsArchive);
                    Note.UpdateOne(NoteId, IsNote);
                    Note.UpdateOne(NoteId, IsTrash);
                    return true;
                }
                else
                {
                    var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                    var IsArchive = Builders<Notes>.Update.Set("IsArchive", false);
                    var IsNote = Builders<Notes>.Update.Set("IsNote", false);
                    var IsTrash = Builders<Notes>.Update.Set("IsTrash", true);
                    Note.UpdateOne(NoteId, IsArchive);
                    Note.UpdateOne(NoteId, IsNote);
                    Note.UpdateOne(NoteId, IsTrash);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool IsPin(string id)
        {
            try
            {
                List<Notes> list = this.Note.Find(notes => notes.NoteId == id).ToList();

                if (list[0].IsPin == true)
                {
                    var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                    var IsPin = Builders<Notes>.Update.Set("IsPin", false);
                    Note.UpdateOne(NoteId, IsPin);
                    return true;
                }
                else
                {
                    var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                    var IsPin = Builders<Notes>.Update.Set("IsPin", true);
                    Note.UpdateOne(NoteId, IsPin);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }



        public bool AddImage(IFormFile file, string noteId, string accountID)
        {
            try
            {
                Account account = new Account(
                           "duhy491cn",
                            "628212385659439",
                               "TUtgRyZZvuTzrTwzMln6S7EXE7g");

                var path = file.OpenReadStream();
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, path),
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                string data = uploadResult.Url.ToString();
                var NoteId = Builders<Notes>.Filter.Eq("NoteId", noteId);
                var Image = Builders<Notes>.Update.Set("Image", data);
                this.Note.UpdateOne(NoteId, Image);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /*public bool AddCollabrator(AddCollabration model, string noteId)
        {
            List<AddCollabration> list = Builders<Notes>.Update.Set("Collabration", model.Collabration);

            var NoteId = Builders<Notes>.Filter.Eq("NoteId", noteId);

            var Collabration = new List<AddCollabration>();
            Builders<Notes>.Update.Set("Collabration", model.Collabration);
            this.Note.UpdateOne(NoteId, Collabration);
            return true;*/



        /*string[] Collabration = new string[5];
        for(int i=0; i<Collabration.Length; i++)
        {
            Builders<Notes>.Update.Set("Collabration", model.Collabration[i]);
            this.Note.UpdateOne(NoteId, Collabration[i]);
        }
        //Collabration = this.Note.InsertOne("Collabration", model.Collabration);
        return true;
        }*/


        public bool AddCollabrator(AddCollabration model, string noteId)
        {
            try
            {
                List<Notes> list = this.Note.Find(notes => notes.NoteId == noteId).ToList();

                var NoteId = Builders<Notes>.Filter.Eq("NoteId", noteId);
                var Collabration = Builders<Notes>.Update.Set("Collabration", model.Collabration);
                this.Note.UpdateMany(NoteId, Collabration);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
 

