using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentSystem.API.Models.DTO
{
    public class QuestionOptionDTO
    {
        public string Name { get; set; }
        public Nullable<bool> IsCorrect { get; set; }
        public int ProfessorId { get; set; }
        public int QuestionId { get; set; }
    }
}