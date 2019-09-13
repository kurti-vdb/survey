using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace checkboxtest.ViewModels
{
    public class AssignedRoles
    {
        public int RoleID { get; set; }
        public string Naam { get; set; }
        public bool Assigned { get; set; }
    }
}