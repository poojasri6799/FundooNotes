using CommonLayer.Model;
using CommonLayer.NoteModels;
using Microsoft.AspNetCore.Http;
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

        public bool IsArchive(string id);

        bool AddReminder(NoteReminder reminder, string noteId);

        bool IsColour(Colour colour, string noteId);

        public bool AddImage(IFormFile file, string noteId, string accountID);

        public bool IsTrash(string id);

        public bool IsPin(string id);

        List<Notes> GetArchive();

        List<Notes> GetTrash();

        List<Notes> SearchNote(string model);

        bool AddCollabrator(AddCollabration model, string noteId);
    }
}
