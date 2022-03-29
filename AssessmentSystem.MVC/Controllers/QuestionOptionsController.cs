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
    public class QuestionOptionsController : Controller
    {
        private AssessmentSystemEntities db = new AssessmentSystemEntities();

        // GET: QuestionOptions
        public async Task<ActionResult> Index()
        {
            var questionOptions = db.QuestionOptions.Include(q => q.Professor).Include(q => q.Question);
            return View(await questionOptions.ToListAsync());
        }

        // GET: QuestionOptions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionOption questionOption = await db.QuestionOptions.FindAsync(id);
            if (questionOption == null)
            {
                return HttpNotFound();
            }
            return View(questionOption);
        }

        // GET: QuestionOptions/Create
        public ActionResult Create()
        {
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId");
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Name");
            return View();
        }

        // POST: QuestionOptions/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuestionOptionId,Name,IsCorrect,RegisterDate,ProfessorId,QuestionId")] QuestionOption questionOption)
        {
            if (ModelState.IsValid)
            {
                db.QuestionOptions.Add(questionOption);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", questionOption.ProfessorId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Name", questionOption.QuestionId);
            return View(questionOption);
        }

        // GET: QuestionOptions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionOption questionOption = await db.QuestionOptions.FindAsync(id);
            if (questionOption == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", questionOption.ProfessorId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Name", questionOption.QuestionId);
            return View(questionOption);
        }

        // POST: QuestionOptions/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuestionOptionId,Name,IsCorrect,ProfessorId,QuestionId")] QuestionOption questionOption)
        {
            if (ModelState.IsValid)
            {
                questionOption.RegisterDate = DateTime.Now;
                db.Entry(questionOption).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", questionOption.ProfessorId);
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Name", questionOption.QuestionId);
            return View(questionOption);
        }

        // GET: QuestionOptions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionOption questionOption = await db.QuestionOptions.FindAsync(id);
            if (questionOption == null)
            {
                return HttpNotFound();
            }
            return View(questionOption);
        }

        // POST: QuestionOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuestionOption questionOption = await db.QuestionOptions.FindAsync(id);
            db.QuestionOptions.Remove(questionOption);
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
