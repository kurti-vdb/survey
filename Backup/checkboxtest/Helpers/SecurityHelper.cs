using checkboxtest.DAL;
using checkboxtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace checkboxtest.Helpers
{
    public class SecurityHelper
    {
        
        /**
         * Method die ingegeven username en paswoord vergelijkt met de waarden in een database om te kunnen inloggen
         * 
         * @Params string username, string paswoord
         * @return true wanneer user mag inloggen, false indien er geen match is
         **/
        public static bool ValidateUser(string userName, string password)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var gebruikers = unitOfWork.GebruikerRepository.GetAll();
            int userOK = (from user in gebruikers
                          where user.Voornaam == userName && user.Paswoord == password
                          select user).Count();
            return userOK == 0 ? false : true;
        }


        /**
         * Static method die een gebruiker ophaalt adv zijn paswoord en gebruikersnaam (voornaam)
         * 
         * @Params string username, string paswoord
         * @return gebruiker-object
         **/
        public static string getUserRole(string username, string paswoord)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var gebruikers = unitOfWork.GebruikerRepository.GetAll();

            var query = (from user in gebruikers
                         where user.Voornaam == username && user.Paswoord == paswoord
                         select user).FirstOrDefault();

            if (query != null)
                return query.Voornaam;
            else
                return null;

        }
    }
}