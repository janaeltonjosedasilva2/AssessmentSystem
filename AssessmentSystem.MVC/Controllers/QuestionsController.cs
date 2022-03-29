using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentSystem.MVC;

namespace AssessmentSystem.MVC.Controllers
{
    public class QuestionsController : Controller
    {
        private AssessmentSystemEntities db = new AssessmentSystemEntities();

        // GET: Questions
        public async Task<ActionResult> Index()
        {
            var questions = db.Questions.Include(q => q.Professor).Include(q => q.Questionnarie);
            return View(await questions.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId");
            ViewBag.QuestionnarieId = new SelectList(db.Questionnaries, "QuestionnarieId", "Name");
            return View();
        }

        // POST: Questions/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuestionId,Name,RegisterDate,ProfessorId,QuestionnarieId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", question.ProfessorId);
            ViewBag.QuestionnarieId = new SelectList(db.Questionnaries, "QuestionnarieId", "Name", question.QuestionnarieId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", question.ProfessorId);
            ViewBag.QuestionnarieId = new SelectList(db.Questionnaries, "QuestionnarieId", "Name", question.QuestionnarieId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuestionId,Name,ProfessorId,QuestionnarieId")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.RegisterDate = DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", question.ProfessorId);
            ViewBag.QuestionnarieId = new SelectList(db.Questionnaries, "QuestionnarieId", "Name", question.QuestionnarieId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Question question = await db.Questions.FindAsync(id);
            db.Questions.Remove(question);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
