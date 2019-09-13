using checkboxtest.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace checkboxtest.Models
{
    public class Question : IEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vraagstelling verplicht.")]
        public string Vraagstelling { get; set; }
        public int Sequentie { get; set; }

        public int QuestionTypeID { get; set; }
        public int AnswerID { get; set; }
        public int GroepID { get; set; }
        public Dictionary<string, string> Answerdictionary = new Dictionary<string, string>();

        public virtual Answer Answer { get; set; }
        public virtual QuestionType QuestionType { get; set;}
        public virtual Groep Groep { get; set; }
    }
}