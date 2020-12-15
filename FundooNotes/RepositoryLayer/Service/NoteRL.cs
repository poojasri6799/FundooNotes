using CommonLayer.Model;
using CommonLayer.NoteModels;
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
        //private readonly IMongoCollection<NoteArchive> NoteArchive;

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

        public bool AddImage(NoteImage image, string noteId)
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

        public List<Notes> GetArchive()
        {
            return this.Note.Find(note => note.IsArchive == true).ToList();
        }

        public bool IsArchive(string id)
        {
            try
            {
                List<Notes> list = this.Note.Find(notes => notes.NoteId == id).ToList();

                if (list[0].IsArchive == true)
                {
                    var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                    var IsArchive = Builders<Notes>.Update.Set("IsArchive", false);
                    Note.UpdateOne(NoteId, IsArchive);
                    return true;
                }
                else
                {
                    var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                    var IsArchive = Builders<Notes>.Update.Set("IsArchive", true);
                    Note.UpdateOne(NoteId, IsArchive);
                    return true;
                }
            }
            catch
            {
                return false;
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
                    var IsTrash = Builders<Notes>.Update.Set("IsTrash", false);
                    Note.UpdateOne(NoteId, IsTrash);
                    return true;
                }
                else
                {
                    var NoteId = Builders<Notes>.Filter.Eq("NoteId", id);
                    var IsTrash = Builders<Notes>.Update.Set("IsTrash", true);
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

    }
 }

