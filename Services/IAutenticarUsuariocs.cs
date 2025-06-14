using APITest_01.Models.DB;
using APITest_01.Models;

namespace APITest_01.Services
{
    public interface IAutenticarUsuario
    {
        public UsuarioApi GetAutenticarUsuario(UsuarioLogin user);

    }
}
