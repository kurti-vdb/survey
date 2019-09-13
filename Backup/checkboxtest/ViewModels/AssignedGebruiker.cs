using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace checkboxtest.ViewModels
{
    public class AssignedGebruiker
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public bool Assigned { get; set; }
    }
}