using checkboxtest.DAL;
using checkboxtest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace checkboxtest.Controllers
{

    [Authorize(Roles = "Administrator, EnqueteAdministrator, User")]
    public class ResultaatController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Resultaat/

        public ActionResult Index(int id)
        {
            List<Enquete> enquetes = new List<Enquete>();
            enquetes = getEnquetesForUser(id);
            return View(enquetes);
        }

        //
        // GET: /Resultaat/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Resultaat/Create

        public ActionResult Create()
        {
            
            return View();
        } 

        //
        // POST: /Resultaat/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Resultaat/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Resultaat/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Resultaat/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Resultaat/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        /**
         * Hulpmethod die enquetes per user teruggeeft
         * Geen gebruik van unitOfWork omdat dit een zeer specifieke query is
         */
        public List<Enquete> getEnquetesForUser(int id)
        {

            EnqueteContext db = new EnqueteContext();
            var query = from e in db.Enquetes
                        where e.OwnerID == id
                        select e;

            return query.ToList();
        }
    }
}
