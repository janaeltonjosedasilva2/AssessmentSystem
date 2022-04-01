using AssessmentSystem.API.Models.DTO;
using AssessmentSystem.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentSystem.API.Manager
{
    public class QuestionOptionManager
    {
        public List<QuestionOptionDTO> GetAll()
        {
            using (var db = new AssessmentSystemEntities())
            {
                return (from q in db.QuestionOptions
                        select new { q.Name, q.IsCorrect, q.ProfessorId, q.QuestionId})
                        .Select(x => new QuestionOptionDTO
                        {
                            Name = x.Name,
                            IsCorrect = x.IsCorrect,
                            ProfessorId = x.ProfessorId,
                            QuestionId = x.QuestionId
                        }).Distinct().ToList();
            }
        }

        public void Delete(int id)
        {
            using (var db = new AssessmentSystemEntities())
            {
                var questionOption = (from q in db.QuestionOptions
                                where q.QuestionOptionId == id
                                select q).FirstOrDefault();
                if (questionOption == null)
                    throw new ArgumentNullException(nameof(questionOption), $"Não foi encontrado referência para ID {id}");
                db.QuestionOptions.Remove(questionOption);
                db.SaveChanges();
            }
        }

        public int Create(QuestionOptionDTO questionDTO)
        {

            using (var db = new AssessmentSystemEntities())
            {
                var questionOption = new QuestionOption()
                {
                    Name = questionDTO.Name,
                    IsCorrect = questionDTO.IsCorrect,
                    ProfessorId = questionDTO.ProfessorId,
                    QuestionId = questionDTO.QuestionId
                };

                db.QuestionOptions.Add(questionOption);
                db.SaveChanges();

                return questionOption.QuestionId;
            }
        }

        public void Update(int id, QuestionOptionDTO questionoptionDTO)
        {
            using (var db = new AssessmentSystemEntities())
            {
                var questionOption = (from q in db.QuestionOptions
                                where q.QuestionOptionId == id
                                select q).FirstOrDefault();

                questionOption.Name = questionoptionDTO.Name;
                questionOption.IsCorrect = questionoptionDTO.IsCorrect;
                questionOption.ProfessorId = questionoptionDTO.ProfessorId;
                questionOption.QuestionId = questionoptionDTO.QuestionId;

                db.SaveChanges();
            }
        }
    }
}