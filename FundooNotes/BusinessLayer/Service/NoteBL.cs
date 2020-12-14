using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        public INoteRL repositoryLayer;

        public NoteBL(INoteRL repositoryLayer)
        {
            this.repositoryLayer = repositoryLayer;
        }



        public Notes AddNote(NoteModel notemodel, string accountID)
        {
            return this.repositoryLayer.AddNote(notemodel, accountID);
        }

        public bool AddReminder(Notes reminder, string noteId)
        {
            return this.repositoryLayer.AddReminder(reminder,noteId);
        }

        public bool DeleteNote(string noteId)
        {
            return this.repositoryLayer.DeleteNote(noteId);
        }

        public Notes EditNotes(Notes notes, string noteId)
        {
            return repositoryLayer.EditNotes(notes, noteId);
        }

        public List<Notes> GetNote(string accountID)
        {
            return this.repositoryLayer.GetNote(accountID);
        }

        /*public bool IsArchive(string id)
        {
            return this.repositoryLayer.IsArchive(id);
        }*/
    }
}
