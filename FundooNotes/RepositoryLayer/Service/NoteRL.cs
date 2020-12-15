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

        public bool AddReminder(Notes reminder, string noteId)
        {
            try
            {
                List<Notes> list = this.Note.Find(notes => notes.NoteId == noteId).ToList();

                var NoteId = Builders<Notes>.Filter.Eq("NoteId", noteId);
                var AddReminder = Builders<Notes>.Update.Set("AddReminder", reminder.AddReminder);
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

        public bool DeleteNote(string noteId)
        {
            this.Note.DeleteOne(note => note.NoteId == noteId);
            return true;
        }

        public Notes EditNotes(Notes notes, string noteId)
        {
             this.Note.ReplaceOne(note => note.NoteId == noteId, notes);
            return notes;
            
             
        }

        public List<Notes> GetNote(string accountID)
        {
            return this.Note.Find(note => note.AccountId == accountID).ToList();
        }

        
    }
    }

