using checkboxtest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace checkboxtest.DAL
{
    public class EnqueteInitializer : DropCreateDatabaseIfModelChanges<EnqueteContext>
    {

        protected override void Seed(EnqueteContext context)
        {
            var roles = new List<Role>
            {
                new Role { Naam = "Administrator",  Omschrijving = "Een administrator kan het systeem beheren en kan dus Gebruikers beheren (aanmaken / verwijderen / aanpassen), Kan alle enquêtes beheren. Kan alle resultaten bekijken" },
                new Role { Naam = "EnqueteAdministrator",  Omschrijving = "Een enquête administrator krijgt een overzicht van al zijn/haar persoonlijke enquêtes en kan deze ook beheren (actief zetten / inactief zetten / aanpassen indien nog niet actief gezet /een user toewijzen, enquête users toewijzen)• Een enquête administrator kan een nieuwe enquête aanmaken / aanpassen• Een enquête administrator kan de resultaten van zijn eigen enquête opvragen" },
                new Role { Naam = "User",  Omschrijving = "Een user kan de resultaten bekijken van de enquêtes waaraan hij/zij is toegewezen voor de verwerking ervan." },
                new Role { Naam = "EnqueteUser",  Omschrijving = "De enquête user kan niet aanloggen in het systeem. Deze gebruikers zijn de groep van mensen die de enquête zullen invullen." },
                 
            };
            roles.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();

            var gebruikers = new List<Gebruiker>
            {
                new Gebruiker { Voornaam = "Eveline", Achternaam = "Wicki", Email = "eveline.wicki@luzern.ch", GsmNummer ="041123456789", TelNummer="041123456789", Paswoord="groept", Roles = new List<Role>()},
                new Gebruiker { Voornaam = "Nina", Achternaam = "Erni", Email = "nina.erni@zurich.ch", GsmNummer ="041123456789", TelNummer="041123456789", Paswoord="groept", Roles = new List<Role>()},
                new Gebruiker { Voornaam = "Claudia", Achternaam = "Annen", Email = "claudia.annen@zurich.ch", GsmNummer ="041123456789", TelNummer="041123456789", Paswoord="groept", Roles = new List<Role>()},
                new Gebruiker { Voornaam = "Andrea", Achternaam = "Rast", Email = "andrea.rast@luzern.ch", GsmNummer ="041123456789", TelNummer="041123456789", Paswoord="groept", Roles = new List<Role>()},
                
            };
            gebruikers.ForEach(s => context.Gebruikers.Add(s));
            context.SaveChanges();

           
            gebruikers[0].Roles.Add(roles[0]);
            gebruikers[0].Roles.Add(roles[3]);//Eveline - Administrator, Enqueteuser 
            gebruikers[1].Roles.Add(roles[1]);//Nina - EnqueteAdministrator
            gebruikers[2].Roles.Add(roles[2]);//Claudia - User
            gebruikers[3].Roles.Add(roles[3]);//Andrea - EnqueteUser
            context.SaveChanges();

            var questionTypes = new List<QuestionType>
            {
                new QuestionType { Omschrijving = "Vrij antwoord" },
                new QuestionType { Omschrijving = "RadioButton Choice" },
                new QuestionType { Omschrijving = "Checkbox Choice" },
            };
            questionTypes.ForEach(t => context.QuestionTypes.Add(t));
            context.SaveChanges();

            var enquetes = new List<Enquete>
            {
                new Enquete 
                { 
                    Titel= "Shampo onderzoek", 
                    OwnerID = 1,
                    Omschrijving = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                    StartDate = new DateTime(2013, 5, 1, 8, 30, 52),
                    EndDate = new DateTime(2013, 9, 1, 8, 30, 52),
                    slotTekst = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                    Active = false,
                    Anoniem = false
                },

                new Enquete 
                { 
                    Titel= "Zeep onderzoek", 
                    OwnerID = 1,
                    Omschrijving = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                    StartDate = new DateTime(2013, 5, 1, 8, 30, 52),
                    EndDate = new DateTime(2013, 10, 1, 8, 30, 52),
                    slotTekst = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                    Active = false,
                    Anoniem = false
                },

                new Enquete 
                { 
                    Titel= "Marktonderzoek ivm Tablets en smartphones", 
                    OwnerID = 2,
                    Omschrijving = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                    StartDate = new DateTime(2013, 5, 1, 8, 30, 52),
                    EndDate = new DateTime(2013, 11, 1, 8, 30, 52),
                    slotTekst = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                    Active = false,
                    Anoniem = false
                },
            };
            enquetes.ForEach(e => context.Enquetes.Add(e));
            context.SaveChanges();




        }
    }
}