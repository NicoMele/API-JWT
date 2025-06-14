using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using APITest_01.Models.DB;
using APITest_01.Configuraciones;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using APITest_01.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace APITest_01.Controllers
{
    [ApiController]
    public class MercadoController : ControllerBase
    {
        private readonly IBuilder builder;
        private readonly ICurrentUser currentUser;
        public MercadoController(IBuilder builder, ICurrentUser currentUser)
        {
            this.builder = builder;
            this.currentUser = currentUser;
            
        }

        [HttpGet("[controller]/GetProductos")]
        [Authorize(Roles = "superadmin")]
        public IActionResult GetProductos()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;//obtengo token que debo validar.
            var user = currentUser.GetCurrentUser(identity);

            if (user != null)
            {
                using (var connection = new SqlConnection(builder.obtenerConnectionString("ConnectionTesting")))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("sp_Producto", connection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            return BadRequest("No hay productos para mostrar");
                        }
                        List<DetalleOutput> ListaSalidaDelDetalle = new List<DetalleOutput>();
                        while (reader.Read())
                        {
                            DetalleOutput Salida = new DetalleOutput();
                            Salida.idProducto = reader.GetInt32(reader.GetOrdinal("idProducto"));
                            Salida.nombreProducto = reader.GetString(reader.GetOrdinal("nombreP"));
                            Salida.precio = reader.GetFloat(reader.GetOrdinal("precio"));
                            ListaSalidaDelDetalle.Add(Salida);
                        }
                        reader.Close();
                        connection.Close();
                        return Ok(ListaSalidaDelDetalle);

                    }
                }
            }
            return BadRequest();
        }

    }
}
