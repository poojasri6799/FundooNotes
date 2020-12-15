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

        public bool AddImage(Notes image, string noteId)
        {
            try
            {
                return this.repositoryLayer.AddImage(image, noteId);
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

        public bool AddReminder(Notes reminder, string noteId)
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

        public bool IsColour(Notes colour, string noteId)
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

        /*public bool IsArchive(string id)
        {
            return this.repositoryLayer.IsArchive(id);
        }*/
    }
}
