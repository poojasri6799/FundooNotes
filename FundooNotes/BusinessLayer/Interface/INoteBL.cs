using CommonLayer.Model;
using CommonLayer.NoteModels;
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

        Notes EditNotes(Notes notes, string noteId);

        bool AddReminder(NoteReminder reminder, string noteId);

        bool IsColour(Colour colour, string noteId);

        bool AddImage(NoteImage image, string noteId);

        public bool IsArchive(string id);

        public bool IsTrash(string id);

        public bool IsPin(string id);

        List<Notes> GetArchive();
    }
}
