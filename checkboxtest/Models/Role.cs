using checkboxtest.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace checkboxtest.Models
{
    public class Role : IEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Naam verplicht.")]
        [MaxLength(50)]
        public string Naam { get; set; }
        public string Omschrijving { get; set; }

        public virtual ICollection<Gebruiker> Gebruiker { get; set; }
    }
}