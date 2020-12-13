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
                IsPin = notemodel.IsPin,
                IsArchive = notemodel.IsArchive,
                IsTrash = notemodel.IsTrash
            };
            this.Note.InsertOne(note);
            return note;
           
        }

        public bool DeleteNote(string noteId)
        {
            this.Note.DeleteOne(note => note.NoteId == noteId);
            return true;
        }

        public bool EditNotes(Notes notes, string noteId)
        {
            this.Note.ReplaceOne(note => note.NoteId == noteId, notes);
            return true;
        }

        public List<Notes> GetNote(string accountID)
        {
            return this.Note.Find(note => note.AccountId == accountID).ToList();
        }
    }
}
