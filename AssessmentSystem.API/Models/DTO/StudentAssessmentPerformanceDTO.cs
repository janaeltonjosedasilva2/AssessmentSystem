using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentSystem.API.Models.DTO
{
    public class StudentAssessmentPerformanceDTO
    {
        public int StudentAssessmentId { get; set; }
        public string StudentName { get; set; }
        public Nullable<decimal> Grade { get; set; }
        public System.DateTime RegisterDate { get; set; }

    }
}