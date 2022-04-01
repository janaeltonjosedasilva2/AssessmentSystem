using AssessmentSystem.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssessmentSystem.MVC.Manager
{
    public class AssessmentManager
    {
        public List<AssessmentDTO> GetAll()
        {
            using (var db = new AssessmentSystemEntities())
            {
                return (from a in db.Assessments
                        select new { a.Name, a.ProfessorId, a.QuestionnarieId })
                        .Select(x => new AssessmentDTO
                        {
                            Name = x.Name,
                            ProfessorId = x.ProfessorId,
                            QuestionnarieId = x.QuestionnarieId
                        }).Distinct().ToList();
            }
        }

        public void Delete(int id)
        {
            using (var db = new AssessmentSystemEntities())
            {
                var assessment = (from a in db.Assessments
                                  where a.AssessmentId == id
                                  select a).FirstOrDefault();
                if (assessment == null)
                    throw new ArgumentNullException(nameof(assessment), $"Não foi encontrado referência para ID {id}");
                db.Assessments.Remove(assessment);
                db.SaveChanges();
            }
        }

        public int Create(AssessmentDTO assessmentDTO)
        {

            using (var db = new AssessmentSystemEntities())
            {
                var assessment = new Assessment()
                {
                    Name = assessmentDTO.Name,
                    ProfessorId = assessmentDTO.ProfessorId,
                    QuestionnarieId = assessmentDTO.QuestionnarieId,
                    RegisterDate = DateTime.Now
                };

                db.Assessments.Add(assessment);
                db.SaveChanges();

                return assessment.AssessmentId;
            }
        }

        public void Update(int id, AssessmentDTO assessmentDTO)
        {
            using(var db = new AssessmentSystemEntities())
            {
                var assessment = (from a in db.Assessments
                                  where a.AssessmentId == id
                                  select a).FirstOrDefault();

                assessment.QuestionnarieId = assessmentDTO.QuestionnarieId;
                assessment.Name = assessmentDTO.Name;
                assessment.ProfessorId = assessmentDTO.ProfessorId;

                db.SaveChanges();
            }
        }
    }
}