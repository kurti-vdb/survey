using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using checkboxtest.Models;
using checkboxtest.DAL;
using checkboxtest.ViewModels;

namespace checkboxtest.Controllers
{
    [Authorize(Roles = "Administrator, EnqueteAdministrator")]
    public class EnqueteController : Controller
    {
        
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Enquete/

        public ViewResult Index()
        {
            var enquetes = unitOfWork.EnqueteRepository.GetAll().Include(e => e.Owner);
            return View(enquetes.ToList());
        }

        //
        // GET: /Enquete/Details/5

        public ViewResult Details(int id)
        {
            Enquete enquete = unitOfWork.EnqueteRepository.GetById(id);
            return View(enquete);
        }

        //
        // GET: /Enquete/Create

        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(unitOfWork.GebruikerRepository.GetAll(), "ID", "Voornaam");
            return View();
        } 

        //
        // POST: /Enquete/Create

        [HttpPost]
        public ActionResult Create(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.EnqueteRepository.Add(enquete);
                unitOfWork.Save();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerID = new SelectList(unitOfWork.GebruikerRepository.GetAll(), "ID", "Voornaam", enquete.OwnerID);
            return View(enquete);
        }
        
        //
        // GET: /Enquete/Edit/5
 
        public ActionResult Edit(int id)
        {
            Enquete enquete = unitOfWork.EnqueteRepository.GetById(id);
            ViewBag.OwnerID = new SelectList(unitOfWork.GebruikerRepository.GetAll(), "ID", "Voornaam", enquete.OwnerID);
            
            return View(enquete);
        }

        //
        // POST: /Enquete/Edit/5

        [HttpPost]
        public ActionResult Edit(Enquete enqueteToUpdate, FormCollection formCollection, string[] enqueteUsers, string[] enqueteAdministrators)
        {
            
            if (ModelState.IsValid)
            {
                unitOfWork.UoWcontext.Entry(enqueteToUpdate).State = EntityState.Modified;
                unitOfWork.Save();
                PopulateAssignedGebruikers(enqueteToUpdate);
                PopulateEnqueteAdministrators(enqueteToUpdate);
                return RedirectToAction("Index");
            }
            if (TryUpdateModel(enqueteToUpdate, "", null, new string[] { "enqueteUsers" }))
            {
                try
                {
                    
                    UpdateGebruikersVeld(enqueteUsers, enqueteToUpdate);
                    unitOfWork.UoWcontext.Entry(enqueteToUpdate).State = EntityState.Modified;
                    unitOfWork.Save();
                   

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }


            ViewBag.OwnerID = new SelectList(unitOfWork.GebruikerRepository.GetAll(), "ID", "Voornaam", enqueteToUpdate.OwnerID);
            
            return View(enqueteToUpdate);
        }

        //
        // GET: /Enquete/Delete/5
 
        public ActionResult Delete(int id)
        {
            Enquete enquete = unitOfWork.EnqueteRepository.GetById(id);
            return View(enquete);
        }

        //
        // POST: /Enquete/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Enquete enquete = unitOfWork.EnqueteRepository.GetById(id);
            unitOfWork.EnqueteRepository.Delete(enquete);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }


        /**
         * Bevolk ENQUETEUSERS veld
         **/ 
        private void PopulateAssignedGebruikers(Enquete enquete)
        {
            var allusers = unitOfWork.GebruikerRepository.GetAll();
            var enqueteUsers = new HashSet<int>(enquete.EnqueteUsers.Select(c => c.ID));
            var viewModel = new List<AssignedGebruiker>();
            foreach (var gebruiker in allusers)
            {
                viewModel.Add(new AssignedGebruiker
                {
                    ID = gebruiker.ID,
                    Naam = gebruiker.Voornaam,
                    Assigned = enqueteUsers.Contains(gebruiker.ID)
                });
            }
            ViewBag.Users = viewModel;
        }

        /**
         * Method die het veld van ENQUETEUSERS controleert op aangevinkte en uitgevinkte gebruikers
         * 
         **/
        private void UpdateGebruikersVeld(string[] selectedUsers, Enquete enquete)
        {
            if (selectedUsers == null)
            {
                enquete.EnqueteUsers = new List<Gebruiker>();
                return;
            }

            var gebruikersPerEnqueteHS = new HashSet<string>(selectedUsers);
            var gebruikersPerEnquete = new HashSet<int>(enquete.EnqueteUsers.Select(g => g.ID));
           foreach (var gebruiker in unitOfWork.GebruikerRepository.GetAll())
            {
                //EnqueteUser is aangevinkt, voeg hem toe
                if (gebruikersPerEnqueteHS.Contains(gebruiker.ID.ToString()))
                {
                    if (!gebruikersPerEnquete.Contains(gebruiker.ID))
                        enquete.EnqueteUsers.Add(gebruiker);
                }
                //EnqueteUser is terug uitgevinkt, verwijder hem
                else
                {
                    if (gebruikersPerEnquete.Contains(gebruiker.ID))
                        enquete.EnqueteUsers.Remove(gebruiker);
                }
            }
        }


        /**
         * Bevolk enqueteADMINISTRATORS veld
         **/
        private void PopulateEnqueteAdministrators(Enquete enquete)
        {
            var allusers = unitOfWork.GebruikerRepository.GetAll();
            var enqueteAdministrators = new HashSet<int>(enquete.EnqueteAdministrators.Select(c => c.ID));
            var viewModel = new List<EnqueteAdministrator>();
            foreach (var gebruiker in allusers)
            {
                viewModel.Add(new EnqueteAdministrator
                {
                    ID = gebruiker.ID,
                    Naam = gebruiker.Voornaam,
                    Assigned = enqueteAdministrators.Contains(gebruiker.ID)
                });
            }
            ViewBag.EnqueteAdministrators = viewModel;
        }
    }
}