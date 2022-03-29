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
    public class AssessmentsController : Controller
    {
        private AssessmentSystemEntities db = new AssessmentSystemEntities();

        // GET: Assessments
        public async Task<ActionResult> Index()
        {
            var assessments = db.Assessments.Include(a => a.Professor).Include(a => a.Questionnarie);
            return View(await assessments.ToListAsync());
        }

        // GET: Assessments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = await db.Assessments.FindAsync(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // GET: Assessments/Create
        public ActionResult Create()
        {
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId");
            ViewBag.QuestionnarieId = new SelectList(db.Questionnaries, "QuestionnarieId", "Name");
            return View();
        }

        // POST: Assessments/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AssessmentId,Name,RegisterDate,ProfessorId,QuestionnarieId")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                db.Assessments.Add(assessment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", assessment.ProfessorId);
            ViewBag.QuestionnarieId = new SelectList(db.Questionnaries, "QuestionnarieId", "Name", assessment.QuestionnarieId);
            return View(assessment);
        }

        // GET: Assessments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = await db.Assessments.FindAsync(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", assessment.ProfessorId);
            ViewBag.QuestionnarieId = new SelectList(db.Questionnaries, "QuestionnarieId", "Name", assessment.QuestionnarieId);
            return View(assessment);
        }

        // POST: Assessments/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssessmentId,Name,ProfessorId,QuestionnarieId")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                assessment.RegisterDate = DateTime.Now;
                db.Entry(assessment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorId = new SelectList(db.Professors, "ProfessorId", "ProfessorId", assessment.ProfessorId);
            ViewBag.QuestionnarieId = new SelectList(db.Questionnaries, "QuestionnarieId", "Name", assessment.QuestionnarieId);
            return View(assessment);
        }

        // GET: Assessments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = await db.Assessments.FindAsync(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // POST: Assessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Assessment assessment = await db.Assessments.FindAsync(id);
            db.Assessments.Remove(assessment);
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
