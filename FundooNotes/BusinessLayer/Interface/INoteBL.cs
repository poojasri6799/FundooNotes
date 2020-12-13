using CommonLayer.Model;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        Notes AddNote(NoteModel notemodel, string accountID);
        
        List<Notes> GetNote(string accountID);

        bool DeleteNote(string noteId);

        bool EditNotes(Notes notes, string noteId);
    }
}
