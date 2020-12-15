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

        Notes EditNotes(Notes notes, string noteId);

        //public bool IsArchive(string id);

        bool AddReminder(Notes reminder, string noteId);

        bool IsColour(Notes colour, string noteId);

        bool AddImage(Notes image, string noteId);
    }
}
