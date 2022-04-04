using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentSystem.API.Models.DTO
{
    public class PersonLoginDTO : PersonDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}