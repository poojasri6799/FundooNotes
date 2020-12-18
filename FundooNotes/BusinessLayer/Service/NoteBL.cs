using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.NoteModels;
using Microsoft.AspNetCore.Http;
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

        public bool AddImage(IFormFile file, string noteId, string accountID)
        {
            try
            {
                return this.repositoryLayer.AddImage(file, noteId, accountID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Notes AddNote(NoteModel notemodel, string accountID)
        {
            try
            {
                return this.repositoryLayer.AddNote(notemodel, accountID);
            }catch(Exception e)
            {
                throw e;
            }
        }

        public bool AddReminder(NoteReminder reminder, string noteId)
        {
            try
            {
                return this.repositoryLayer.AddReminder(reminder, noteId);
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
                return this.repositoryLayer.DeleteNote(noteId);
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
                return repositoryLayer.EditNotes(notes, noteId);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public List<Notes> GetNote(string accountID)
        {
            try
            {
                return this.repositoryLayer.GetNote(accountID);
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
                return this.repositoryLayer.IsColour(colour, noteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsArchive(string id)
        {
            try
            {
                return this.repositoryLayer.IsArchive(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsTrash(string id)
        {
            try
            {
                return this.repositoryLayer.IsTrash(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsPin(string id)
        {
            try
            {
                return this.repositoryLayer.IsPin(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> GetArchive()
        {
            try
            {
                return this.repositoryLayer.GetArchive();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> GetTrash()
        {
            try
            {
                return this.repositoryLayer.GetTrash();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> SearchNote(string search)
        {
            try
            {
               return this.repositoryLayer.SearchNote(search);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool AddCollabrator(AddCollabration model, string noteId)
        {
            try
            {
                return this.repositoryLayer.AddCollabrator(model, noteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Notes> SearchCollabrator()
        {
            try
            {
                return repositoryLayer.SearchCollabrator();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
