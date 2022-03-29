using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentSystem.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new AssessmentSystemEntities())
            {
                Assessment assessment = new Assessment();
                var test = from a in db.Assessments
                           where a.AssessmentId == 1
                           select a;
                var test1 = test.First();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}