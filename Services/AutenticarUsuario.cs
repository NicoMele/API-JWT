using APITest_01.Models;
using APITest_01.Models.DB;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APITest_01.Services
{
    public class AutenticarUsuario : IAutenticarUsuario 
    {
        public UsuarioApi GetAutenticarUsuario(UsuarioLogin user)
        {
            var usuario = new UsuarioApi();

            using (var contextoDB = new LoginUserContext())
            {
                usuario = contextoDB.UsuarioApis.ToList().Where(x => x.Nombre.ToLower() == user.nombre && x.Contraseña.ToLower() == user.Contraseña).FirstOrDefault();
            }

            return usuario;
        }
        
    }
}
