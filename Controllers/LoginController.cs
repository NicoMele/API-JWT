using Microsoft.AspNetCore.Mvc;
using APITest_01.Models.DB;
using APITest_01.Configuraciones;
using System.Runtime.CompilerServices;
using APITest_01.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using APITest_01.Services;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;

namespace APITest_01.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
       
        private readonly IAutenticarUsuario autenticado;
        private readonly IGenerateToken token;
        private readonly IBuilder builder;
        private readonly ICurrentUser currentUser;

        public LoginController(IGenerateToken token, IAutenticarUsuario autenticado,IBuilder builder, ICurrentUser currentUser)
        {   
            this.autenticado= autenticado;
            this.token = token;
            this.builder = builder;
            this.currentUser = currentUser;
        }

        [HttpPost("[controller]/iniciarSesion")]
        public IActionResult IniciarSesion(UsuarioLogin user)
        {
            using (var connection = new SqlConnection(builder.obtenerConnectionString("ConnectionTesting")))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM papa";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                        }
                    }
                }
            }
            var userAutenticado = this.autenticado.GetAutenticarUsuario(user);

            if (userAutenticado != null)
            {
                return Ok((this.token.GetToken(userAutenticado)));
            }
            return NotFound("El usuario no se encuentra en la base de datos");
        }

    }
}