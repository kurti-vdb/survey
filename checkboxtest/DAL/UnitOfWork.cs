using checkboxtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace checkboxtest.DAL
{
    public class UnitOfWork : IDisposable
    {
        
        private GenericRepository<Gebruiker> gebruikerRepository;
        private GenericRepository<Role> roleRepository;
        private GenericRepository<Enquete> enqueteRepository;
        private GenericRepository<Groep> groepRepository;
        private GenericRepository<Question> questionRepository;
        private GenericRepository<Answer> answerRepository;
        private GenericRepository<QuestionType> questionTypeRepository;

        public EnqueteContext UoWcontext { get; set; }
        private bool disposed = false; 

        public GenericRepository<Gebruiker> GebruikerRepository
        {
            get
            {
                if (this.gebruikerRepository == null)
                {
                    this.gebruikerRepository = new GenericRepository<Gebruiker>(UoWcontext);
                }
                return gebruikerRepository;
            }
        }

        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new GenericRepository<Role>(UoWcontext);
                }
                return roleRepository;
            }
        }

        public GenericRepository<Enquete> EnqueteRepository
        {
            get
            {
                if (this.enqueteRepository == null)
                {
                    this.enqueteRepository = new GenericRepository<Enquete>(UoWcontext);
                }
                return enqueteRepository;
            }
        }

        public GenericRepository<Groep> GroepRepository
        {
            get
            {
                if (this.groepRepository == null)
                {
                    this.groepRepository = new GenericRepository<Groep>(UoWcontext);
                }
                return groepRepository;
            }
        }

        public GenericRepository<Question> QuestionRepository
        {
            get
            {
                if (this.questionRepository == null)
                {
                    this.questionRepository = new GenericRepository<Question>(UoWcontext);
                }
                return questionRepository;
            }
        }

        public GenericRepository<Answer> AnswerRepository
        {
            get
            {
                if (this.answerRepository == null)
                {
                    this.answerRepository = new GenericRepository<Answer>(UoWcontext);
                }
                return answerRepository;
            }
        }

        public GenericRepository<QuestionType> QuestionTypeRepository
        {
            get
            {
                if (this.questionTypeRepository == null)
                {
                    this.questionTypeRepository = new GenericRepository<QuestionType>(UoWcontext);
                }
                return questionTypeRepository;
            }
        }

        public UnitOfWork()
        {
            UoWcontext = new EnqueteContext();
        }

        public void Save()
        {
            UoWcontext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UoWcontext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}