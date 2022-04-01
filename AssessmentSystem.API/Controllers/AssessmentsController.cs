using AssessmentSystem.MVC;
using AssessmentSystem.MVC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentSystem.API.Controllers
{
    public class AssessmentsController : Controller
    {
        AssessmentManager assessmentManager = new AssessmentManager();
        /// <summary>
        /// Listagem de todas as provas.
        /// </summary>
        /// <returns>Lista de provas.</returns>
        public List<Assessment> GetAll()
        {
            return assessmentManager.GetAll();
        }
    }
}