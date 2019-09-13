using checkboxtest.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace checkboxtest.Models
{
    public class Groep : IEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Titel verplicht.")]
        public string Titel { get; set; }
        public int EnqueteID { get; set; }

        public virtual Enquete Enquete { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}