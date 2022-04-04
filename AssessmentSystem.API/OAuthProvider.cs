using AssessmentSystem.API.Manager;
using AssessmentSystem.API.Models.DTO;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AssessmentSystem.API
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly PersonLoginManager personLoginManager = new PersonLoginManager();

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            return Task.Factory.StartNew(() =>
            {
                string email = context.UserName;
                string password = Regex.Replace(context.Password, @"[\f\n\r\t\v]", "");
                //string password = JObject.Parse(context.Password); ;


                PersonLoginDTO person = personLoginManager.ValidateLogin(email, password);

                if (person != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, person.Login),
                        new Claim("PersonId", person.PersonId.ToString()),
                        //new Claim(ClaimTypes.Role, user.Role)
                    };

                    ClaimsIdentity OAuthIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);

                    context.Validated(new Microsoft.Owin.Security.AuthenticationTicket(OAuthIdentity, new Microsoft.Owin.Security.AuthenticationProperties() { }));

                }
                else
                {
                    context.SetError("error", "Usuário não encontrado");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}