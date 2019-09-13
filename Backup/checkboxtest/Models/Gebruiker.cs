using checkboxtest.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace checkboxtest.Models
{
    public class Gebruiker : IEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Voornaam verplicht")]
        [MaxLength(50)]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Achternaam verplicht")]
        [MaxLength(50)]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Email verplicht")]
        [MaxLength(70)]
        public string Email { get; set; }

        public string TelNummer { get; set; }
        public string GsmNummer { get; set; }

        [Required(ErrorMessage = "Paswoord verplicht")]
        [MaxLength(50)]
        public string Paswoord { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Enquete> Enquetes { get; set; }
    }
}