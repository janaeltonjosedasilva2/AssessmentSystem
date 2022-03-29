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
    public class StudentAssessmentPerformancesController : Controller
    {
        private AssessmentSystemEntities db = new AssessmentSystemEntities();

        // GET: StudentAssessmentPerformances
        public async Task<ActionResult> Index()
        {
            var studentAssessmentPerformances = db.StudentAssessmentPerformances.Include(s => s.StudentAssessment);
            return View(await studentAssessmentPerformances.ToListAsync());
        }

        // GET: StudentAssessmentPerformances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssessmentPerformance studentAssessmentPerformance = await db.StudentAssessmentPerformances.FindAsync(id);
            if (studentAssessmentPerformance == null)
            {
                return HttpNotFound();
            }
            return View(studentAssessmentPerformance);
        }

        // GET: StudentAssessmentPerformances/Create
        public ActionResult Create()
        {
            ViewBag.StudentAssessmentId = new SelectList(db.StudentAssessments, "StudentAssessmentId", "StudentAssessmentId");
            return View();
        }

        // POST: StudentAssessmentPerformances/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentAssessmentPerformanceId,StudentAssessmentId,Grade,RegisterDate")] StudentAssessmentPerformance studentAssessmentPerformance)
        {
            if (ModelState.IsValid)
            {
                db.StudentAssessmentPerformances.Add(studentAssessmentPerformance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StudentAssessmentId = new SelectList(db.StudentAssessments, "StudentAssessmentId", "StudentAssessmentId", studentAssessmentPerformance.StudentAssessmentId);
            return View(studentAssessmentPerformance);
        }

        // GET: StudentAssessmentPerformances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssessmentPerformance studentAssessmentPerformance = await db.StudentAssessmentPerformances.FindAsync(id);
            if (studentAssessmentPerformance == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentAssessmentId = new SelectList(db.StudentAssessments, "StudentAssessmentId", "StudentAssessmentId", studentAssessmentPerformance.StudentAssessmentId);
            return View(studentAssessmentPerformance);
        }

        // POST: StudentAssessmentPerformances/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentAssessmentPerformanceId,StudentAssessmentId,Grade")] StudentAssessmentPerformance studentAssessmentPerformance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAssessmentPerformance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StudentAssessmentId = new SelectList(db.StudentAssessments, "StudentAssessmentId", "StudentAssessmentId", studentAssessmentPerformance.StudentAssessmentId);
            return View(studentAssessmentPerformance);
        }

        // GET: StudentAssessmentPerformances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAssessmentPerformance studentAssessmentPerformance = await db.StudentAssessmentPerformances.FindAsync(id);
            if (studentAssessmentPerformance == null)
            {
                return HttpNotFound();
            }
            return View(studentAssessmentPerformance);
        }

        // POST: StudentAssessmentPerformances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StudentAssessmentPerformance studentAssessmentPerformance = await db.StudentAssessmentPerformances.FindAsync(id);
            db.StudentAssessmentPerformances.Remove(studentAssessmentPerformance);
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
