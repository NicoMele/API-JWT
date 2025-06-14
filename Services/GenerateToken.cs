using APITest_01.Models.DB;
using APITest_01.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITest_01.Services
{
    public class GenerateToken : ControllerBase ,IGenerateToken 
    {
        private readonly IConfiguration configuration;
        public GenerateToken(IConfiguration configuration){
            this.configuration = configuration;
        }
        public string GetToken(UsuarioApi user)
        {
            HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, ClaimsPrincipal.Current);
            var jwt = this.configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]//datos que encapsula el token
            {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),//identifica al principal que es el del JWT
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//identificador unico de token
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),//identifica el momento en que se envió el JWT.
                    new Claim (ClaimTypes.Name,user.Nombre),
                    new Claim (ClaimTypes.Email,user.Email),
                    new Claim (ClaimTypes.Role,user.Rol),
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));//obtenemos key 
            var sigin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//firma token

            var token = new JwtSecurityToken( // creamos token en si
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: sigin
                );
           
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
