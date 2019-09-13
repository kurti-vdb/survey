using checkboxtest.DAL;
using checkboxtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace checkboxtest.Helpers
{
    public class RoleHelper : RoleProvider
    {

        /** 
         * Method die alle rollen van een gebruiker opvraagt
         **/

        public override string[] GetRolesForUser(string username)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var gebruiker = unitOfWork.GebruikerRepository.GetAll().FirstOrDefault(x => x.Voornaam == username);
                if (gebruiker == null)
                {
                    return null;
                }
                else
                {
                    string[] rollenLijst = gebruiker.Roles.Select(x => x.Naam).ToArray();
                    return rollenLijst;
                }
            }

        }





        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}