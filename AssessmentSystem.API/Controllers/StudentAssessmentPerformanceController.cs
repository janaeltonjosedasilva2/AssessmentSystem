using AssessmentSystem.API.Manager;
using AssessmentSystem.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AssessmentSystem.API.Controllers
{
    public class StudentAssessmentPerformanceController : ApiController
    {
        StudentAssessmentPerformanceManager studentManager = new StudentAssessmentPerformanceManager();

        /// <summary>
        /// Retorna listagem de questões.
        /// </summary>
        /// <returns>Enumerável de provas.</returns>
        public IEnumerable<StudentAssessmentPerformanceDTO> GetAll()
        {
            return studentManager.GetAll();
        }
    }
}
