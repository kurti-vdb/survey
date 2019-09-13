using checkboxtest.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace checkboxtest.Models
{
    public class QuestionType : IEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Benaming verplicht.")]
        [MaxLength(50)]
        public string Omschrijving { get; set; }
        

    }
}