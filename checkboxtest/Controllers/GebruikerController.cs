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
    [Authorize(Roles = "Administrator")]
    public class GebruikerController : Controller
    {
        //private EnqueteContext db = new EnqueteContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Gebruiker/

        public ViewResult Index()
        {
            var gebruikers =  unitOfWork.GebruikerRepository.GetAll();
            return View(gebruikers);
        }

        //
        // GET: /Gebruiker/Details/5

        public ViewResult Details(int id)
        {
            Gebruiker gebruiker = unitOfWork.GebruikerRepository.GetById(id);
            //Gebruiker gebruiker = db.Gebruikers.Find(id);
            return View(gebruiker);
        }

        //
        // GET: /Gebruiker/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Gebruiker/Create

        [HttpPost]
        public ActionResult Create(Gebruiker gebruiker, FormCollection formCollection, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.GebruikerRepository.Add(gebruiker);
                unitOfWork.Save();
                //db.Gebruikers.Add(gebruiker);
                //db.SaveChanges();
                
                return RedirectToAction("Index");  
            }

            return View(gebruiker);
        }
        
        //
        // GET: /Gebruiker/Edit/5
 
        public ActionResult Edit(int id)
        {
            //Gebruiker gebruiker = db.Gebruikers.Find(id);
            Gebruiker gebruiker = unitOfWork.GebruikerRepository.GetById(id);
            PopulateAssignedRoles(gebruiker);
            return View(gebruiker);
        }

        //
        // POST: /Gebruiker/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedRoles)
        {
            Gebruiker gebruikerToUpdate = unitOfWork.GebruikerRepository.GetById(id);
            if (TryUpdateModel(gebruikerToUpdate, "", null, new string[] { "Roles" }))
            {
                try
                {
                    UpdateGebruikersRollen(selectedRoles, gebruikerToUpdate);

                    //db.Entry(gebruikerToUpdate).State = EntityState.Modified;
                    unitOfWork.UoWcontext.Entry(gebruikerToUpdate).State = EntityState.Modified;
                    unitOfWork.Save();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                }
            }
            PopulateAssignedRoles(gebruikerToUpdate);
            return View(gebruikerToUpdate);
        }


        //
        // GET: /Gebruiker/Delete/5
 
        public ActionResult Delete(int id)
        {
            Gebruiker gebruiker = unitOfWork.GebruikerRepository.GetById(id);
            return View(gebruiker);
        }

        //
        // POST: /Gebruiker/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
           
            Gebruiker gebruiker = unitOfWork.GebruikerRepository.GetById(id);
            unitOfWork.GebruikerRepository.Delete(gebruiker);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private void PopulateAssignedRoles(Gebruiker gebruiker)
        {
           
            var allroles = unitOfWork.RoleRepository.GetAll();
            var gebruikersRollen = new HashSet<int>(gebruiker.Roles.Select(c => c.ID));
            var viewModel = new List<AssignedRoles>();
            foreach (var rol in allroles)
            {
                viewModel.Add(new AssignedRoles
                {
                    RoleID = rol.ID,
                    Naam = rol.Naam,
                    Assigned = gebruikersRollen.Contains(rol.ID)
                });
            }
            ViewBag.Roles = viewModel;
        }

        private void UpdateGebruikersRollen(string[] selectedRoles, Gebruiker gebruikerToUpdate)
        {
            if (selectedRoles == null)
            {
                gebruikerToUpdate.Roles = new List<Role>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var gebruikersRollen = new HashSet<int>
                (gebruikerToUpdate.Roles.Select(c => c.ID));
            foreach (var rol in unitOfWork.RoleRepository.GetAll())
            {
                if (selectedRolesHS.Contains(rol.ID.ToString()))
                {
                    if (!gebruikersRollen.Contains(rol.ID))
                    {
                        gebruikerToUpdate.Roles.Add(rol);
                    }
                }
                else
                {
                    if (gebruikersRollen.Contains(rol.ID))
                    {
                        gebruikerToUpdate.Roles.Remove(rol);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}