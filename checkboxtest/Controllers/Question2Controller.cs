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
    public class Question2Controller : Controller
    {
        private EnqueteContext db = new EnqueteContext();

        //
        // GET: /Question2/

        public ViewResult Index()
        {
            var questions = db.Questions.Include(q => q.Answer).Include(q => q.QuestionType).Include(q => q.Groep);
            return View(questions.ToList());
        }

        //
        // GET: /Question2/Details/5

        public ViewResult Details(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // GET: /Question2/Create

        public ActionResult Create()
        {
            ViewBag.AnswerID = new SelectList(db.Answers, "ID", "AnswerValue");
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "ID", "Omschrijving");
            ViewBag.GroepID = new SelectList(db.Groepen, "ID", "Titel");
            return View();
        } 

        //
        // POST: /Question2/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AnswerID = new SelectList(db.Answers, "ID", "AnswerValue", question.AnswerID);
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "ID", "Omschrijving", question.QuestionTypeID);
            ViewBag.GroepID = new SelectList(db.Groepen, "ID", "Titel", question.GroepID);
            return View(question);
        }
        
        //
        // GET: /Question2/Edit/5
 
        public ActionResult Edit(int id)
        {
            Question question = db.Questions.Find(id);
            ViewBag.AnswerID = new SelectList(db.Answers, "ID", "AnswerValue", question.AnswerID);
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "ID", "Omschrijving", question.QuestionTypeID);
            ViewBag.GroepID = new SelectList(db.Groepen, "ID", "Titel", question.GroepID);
            return View(question);
        }

        //
        // POST: /Question2/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnswerID = new SelectList(db.Answers, "ID", "AnswerValue", question.AnswerID);
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "ID", "Omschrijving", question.QuestionTypeID);
            ViewBag.GroepID = new SelectList(db.Groepen, "ID", "Titel", question.GroepID);
            return View(question);
        }

        //
        // GET: /Question2/Delete/5
 
        public ActionResult Delete(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // POST: /Question2/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}