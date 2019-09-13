using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using checkboxtest.Models;
using checkboxtest.DAL;

namespace checkboxtest.Controllers
{
    [Authorize(Roles = "Administrator, EnqueteAdministrator")]
    public class GroepController : Controller
    {
        
        UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Groep/

        public ViewResult Index()
        {
            var groepen = unitOfWork.GroepRepository.GetAll();
            return View(groepen.ToList());
        }

        //
        // GET: /Groep/Details/5

        public ViewResult Details(int id)
        {
            Groep groep = unitOfWork.GroepRepository.GetById(id);
            return View(groep);
        }

        //
        // GET: /Groep/Create

        public ActionResult Create()
        {
            ViewBag.EnqueteID = new SelectList(unitOfWork.EnqueteRepository.GetAll(), "ID", "Titel");
            return View();
        } 

        //
        // POST: /Groep/Create

        [HttpPost]
        public ActionResult Create(Groep groep)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.GroepRepository.Add(groep);
                unitOfWork.Save();
                return RedirectToAction("Index");  
            }

            ViewBag.EnqueteID = new SelectList(unitOfWork.EnqueteRepository.GetAll(), "ID", "Titel", groep.EnqueteID);
            return View(groep);
        }
        
        //
        // GET: /Groep/Edit/5
 
        public ActionResult Edit(int id)
        {
            Groep groep = unitOfWork.GroepRepository.GetById(id);
            ViewBag.EnqueteID = new SelectList(unitOfWork.EnqueteRepository.GetAll(), "ID", "Titel", groep.EnqueteID);
            return View(groep);
        }

        //
        // POST: /Groep/Edit/5

        [HttpPost]
        public ActionResult Edit(Groep groep)
        {
            EnqueteContext db = new EnqueteContext();

            if (ModelState.IsValid)
            {
                db.Entry(groep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnqueteID = new SelectList(db.Enquetes, "ID", "Titel", groep.EnqueteID);
            return View(groep);
        }

        //
        // GET: /Groep/Delete/5
 
        public ActionResult Delete(int id)
        {
            EnqueteContext db = new EnqueteContext();
            Groep groep = db.Groepen.Find(id);
            return View(groep);
        }

        //
        // POST: /Groep/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            EnqueteContext db = new EnqueteContext();
            Groep groep = db.Groepen.Find(id);
            db.Groepen.Remove(groep);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            
            EnqueteContext db = new EnqueteContext();
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}