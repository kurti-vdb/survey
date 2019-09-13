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
    public class QuestionController : Controller
    {
        private EnqueteContext db = new EnqueteContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Question/

        public ViewResult Index()
        {
            var questions = unitOfWork.QuestionRepository.GetAll().Include(q => q.Answer).Include(q => q.QuestionType).Include(q => q.Groep);
            return View(questions.ToList());
        }

        //
        // GET: /Question/Details/5

        public ViewResult Details(int id)
        {
            Question question = unitOfWork.QuestionRepository.GetById(id);
            return View(question);
        }

        //
        // GET: /Question/Create

        public ActionResult Create()
        {
            ViewBag.AnswerID = new SelectList(unitOfWork.AnswerRepository.GetAll(), "ID", "AnswerValue");
            ViewBag.QuestionTypeID = new SelectList(unitOfWork.QuestionTypeRepository.GetAll(), "ID", "Omschrijving");
            ViewBag.GroepID = new SelectList(unitOfWork.GroepRepository.GetAll(), "ID", "Titel");
            return View();
        } 

        //
        // POST: /Question/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.QuestionRepository.Add(question);
                unitOfWork.Save();
                return RedirectToAction("Index");  
            }

            ViewBag.AnswerID = new SelectList(unitOfWork.AnswerRepository.GetAll(), "ID", "AnswerValue", question.AnswerID);
            ViewBag.QuestionTypeID = new SelectList(unitOfWork.QuestionTypeRepository.GetAll(), "ID", "Omschrijving", question.QuestionTypeID);
            ViewBag.GroepID = new SelectList(unitOfWork.GroepRepository.GetAll(), "ID", "Titel", question.GroepID);
            return View(question);
        }
        
        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int id)
        {
            Question question = db.Questions.Find(id);
            ViewBag.AnswerID = new SelectList(unitOfWork.AnswerRepository.GetAll(), "ID", "AnswerValue", question.AnswerID);
            ViewBag.QuestionTypeID = new SelectList(unitOfWork.QuestionTypeRepository.GetAll(), "ID", "Omschrijving", question.QuestionTypeID);
            ViewBag.GroepID = new SelectList(unitOfWork.GroepRepository.GetAll(), "ID", "Titel", question.GroepID);
            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.UoWcontext.Entry(question).State = EntityState.Modified;
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.AnswerID = new SelectList(unitOfWork.AnswerRepository.GetAll(), "ID", "AnswerValue", question.AnswerID);
            ViewBag.QuestionTypeID = new SelectList(unitOfWork.QuestionTypeRepository.GetAll(), "ID", "Omschrijving", question.QuestionTypeID);
            ViewBag.GroepID = new SelectList(unitOfWork.GroepRepository.GetAll(), "ID", "Titel", question.GroepID);
            return View(question);
        }

        //
        // GET: /Question/Delete/5
 
        public ActionResult Delete(int id)
        {
            Question question = unitOfWork.QuestionRepository.GetById(id);
            return View(question);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = unitOfWork.QuestionRepository.GetById(id);
            unitOfWork.QuestionRepository.Delete(question);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}