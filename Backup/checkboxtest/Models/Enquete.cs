using checkboxtest.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace checkboxtest.Models
{
    public class Enquete : IEntity
    {
        
        public int ID { get; set; }

        [Required(ErrorMessage = "Titel verplicht.")]
        [MaxLength(50)]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Omschrijving verplicht.")]
        public string Omschrijving { get; set; }
        public bool Active { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Anoniem { get; set; }
        public string slotTekst { get; set; }

        [Required(ErrorMessage = "Owner verplicht.")]
        public int OwnerID { get; set; }

        public virtual Gebruiker Owner { get; set; }
        public virtual ICollection<Gebruiker> EnqueteUsers { get; set; }
        public virtual ICollection<Groep> Groepen { get; set;}
        public virtual ICollection<Gebruiker> EnqueteAdministrators { get; set; }

        public Enquete()
        {
            this.EnqueteUsers = new HashSet<Gebruiker>();
            this.EnqueteAdministrators = new HashSet<Gebruiker>();
            this.Groepen = new HashSet<Groep>();
        }
    }
}