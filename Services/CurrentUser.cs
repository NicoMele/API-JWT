using APITest_01.Models.DB;
using APITest_01.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace APITest_01.Services
{
    public class CurrentUser : ControllerBase, ICurrentUser
    {
        public UsuarioApi GetCurrentUser(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, ClaimsPrincipal.Current);
                if (Jwt.validarToken(identity))
                {
                    var userClaims = identity.Claims;

                    return new UsuarioApi
                    {
                        Nombre = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value,
                        Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                        Rol = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value,
                    };
                }

            }
            return null;

        }
    }
}
