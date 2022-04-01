using AssessmentSystem.API.Models.DTO;
using AssessmentSystem.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentSystem.API.Manager
{
    public class QuestionManager
    {
        public List<QuestionDTO> GetAll()
        {
            using (var db = new AssessmentSystemEntities())
            {
                return (from q in db.Questions
                        select new { q.Name, q.ProfessorId, q.QuestionnarieId })
                        .Select(x => new QuestionDTO
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
                var question = (from q in db.Questions
                                  where q.QuestionId == id
                                  select q).FirstOrDefault();
                if (question == null)
                    throw new ArgumentNullException(nameof(question), $"Não foi encontrado referência para ID {id}");
                db.Questions.Remove(question);
                db.SaveChanges();
            }
        }

        public int Create(QuestionDTO questionDTO)
        {

            using (var db = new AssessmentSystemEntities())
            {
                var question = new Question()
                {
                    Name = questionDTO.Name,
                    ProfessorId = questionDTO.ProfessorId,
                    QuestionnarieId = questionDTO.QuestionnarieId,
                    RegisterDate = DateTime.Now
                };

                db.Questions.Add(question);
                db.SaveChanges();

                return question.QuestionId;
            }
        }

        public void Update(int id, QuestionDTO questionDTO)
        {
            using (var db = new AssessmentSystemEntities())
            {
                var question = (from q in db.Questions
                                  where q.QuestionId == id
                                  select q).FirstOrDefault();

                question.QuestionnarieId = questionDTO.QuestionnarieId;
                question.Name = questionDTO.Name;
                question.ProfessorId = questionDTO.ProfessorId;

                db.SaveChanges();
            }
        }
    }
}