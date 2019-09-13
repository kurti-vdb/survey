using checkboxtest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace checkboxtest.DAL
{
    public class EnqueteContext : DbContext, IDbContext
    {
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Enquete> Enquetes { get; set; }
        public DbSet<Groep> Groepen { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }


        //Zorgt ervoor dat de gegenereerde tabellen in de database geen meervoud hebben
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }


    }
}