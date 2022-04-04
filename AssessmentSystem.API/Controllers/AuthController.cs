using AssessmentSystem.API.Manager;
using AssessmentSystem.API.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace AssessmentSystem.API.Controllers
{
    public class AuthController : ApiController
    {
        private readonly PersonLoginManager personLoginManager = new PersonLoginManager();

        private Object createToken(string personName, string id, string role)
        {
            string key = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw=="; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("PersonId", id));
            permClaims.Add(new Claim(ClaimTypes.Name, personName));  //PEGAR AS ROLES CORRETAS

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return new { data = jwt_token };
        }

        /// <summary>
        /// Gerar token para requisições.
        /// </summary>
        /// <param name="personLoginDTO"></param>
        /// <returns>Token.</returns>
        [HttpPost]
        public Object SignIn([FromBody] PersonLoginDTO personLoginDTO)
        {
            PersonLoginDTO person = personLoginManager.ValidateLogin(personLoginDTO.Login, personLoginDTO.Password);
            if (person == null)
            {
                return new Exception("Usuário ou senha invalidos");
            }

            return createToken(person.Login, person.PersonId.ToString(), "Administrador");
        }
    }
}
