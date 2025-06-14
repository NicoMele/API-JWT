using System;
using System.Collections.Generic;

namespace APITest_01.Models.DB
{
    public partial class UsuarioApi
    {
        public int IdUser { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
