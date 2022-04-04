using AssessmentSystem.API.Models.DTO;
using AssessmentSystem.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentSystem.API.Manager
{
    public class PersonLoginManager
    {
        public PersonLoginDTO ValidateLogin(string login, string password)
        {
            using(var db = new AssessmentSystemEntities())
            {
                var person = (from pl in db.PersonLogins
                                   join p in db.People on pl.PersonId equals p.PersonId
                            where pl.Login == login && pl.Password == password
                            select new { p.PersonId, p.Name, pl.Login }).
                            Select(x => new PersonLoginDTO
                            {
                                PersonId = x.PersonId,
                                Name = x.Name,
                                Login = x.Login
                            }).FirstOrDefault();

                return person;
            }
        }
    }
}