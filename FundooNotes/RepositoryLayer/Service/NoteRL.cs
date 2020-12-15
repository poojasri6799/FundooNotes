using CommonLayer.Model;
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
                    Collabration = notemodel.Collabration
                };
                this.Note.InsertOne(note);
                return note;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool AddReminder(Notes reminder, string noteId)
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

        public bool IsColour(Notes colour, string noteId)
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

        public bool AddImage(Notes image, string noteId)
        {
            try
            {
                List<Notes> list = this.Note.Find(notes => notes.NoteId == noteId).ToList();

                var NoteId = Builders<Notes>.Filter.Eq("NoteId", noteId);
                var Image = Builders<Notes>.Update.Set("Image", image.Image);
                this.Note.UpdateOne(NoteId, Image);
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
            return this.Note.Find(note => note.AccountId == accountID).ToList();
        }

        
    }
 }

