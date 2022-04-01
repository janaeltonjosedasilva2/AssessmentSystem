using AssessmentSystem.API.Models.DTO;
using AssessmentSystem.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentSystem.API.Manager
{
    public class StudentAssessmentPerformanceManager
    {
        public List<StudentAssessmentPerformanceDTO> GetAll()
        {
            using (var db = new AssessmentSystemEntities())
            {
                return (from sap in db.StudentAssessmentPerformances
                        join sa in db.StudentAssessments on sap.StudentAssessmentId equals sa.StudentAssessmentId
                        join st in db.Students on sa.StudentId equals st.StudentId
                        join p in db.People on st.PersonId equals p.PersonId
                        select new { sap.StudentAssessmentId, p.Name, sap.Grade, sap.RegisterDate })
                        .Select(x => new StudentAssessmentPerformanceDTO
                        {
                            StudentAssessmentId = x.StudentAssessmentId,
                            StudentName = x.Name,
                            Grade = x.Grade,
                            RegisterDate = x.RegisterDate
                        }).Distinct().ToList();
            }
        }
    }
}