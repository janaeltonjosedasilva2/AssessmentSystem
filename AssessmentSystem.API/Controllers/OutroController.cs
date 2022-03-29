using AssessmentSystem.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentSystem.API.Controllers
{
    public class OutroController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            using (var db = new AssessmentSystemEntities())
            {
                var test = from a in db.Assessments
                           where a.AssessmentId == 1
                           select a;
                var test1 = test.First();
            }

            return View();
        }
    }
}
