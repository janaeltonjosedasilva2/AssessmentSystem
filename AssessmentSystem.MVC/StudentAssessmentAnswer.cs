//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssessmentSystem.MVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentAssessmentAnswer
    {
        public int StudentAssessmentAnswerId { get; set; }
        public int StudentAssessmentId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public int QuestionOptionId { get; set; }
    
        public virtual QuestionOption QuestionOption { get; set; }
        public virtual StudentAssessment StudentAssessment { get; set; }
    }
}
