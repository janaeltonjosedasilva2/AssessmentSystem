using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentSystem.API.Models.DTO
{
    public class QuestionDTO
    {
        public string Name { get; set; }
        public int ProfessorId { get; set; }
        public int QuestionnarieId { get; set; }
    }
}