using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        Notes AddNote(NoteModel notemodel, string accountID);
        List<Notes> GetNote(string accountID);
        bool DeleteNote(string noteId);
        bool EditNotes(Notes notes, string noteId);
    }
}
