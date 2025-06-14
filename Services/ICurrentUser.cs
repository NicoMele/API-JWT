using APITest_01.Models.DB;
using System.Security.Claims;

namespace APITest_01.Services
{
    public interface ICurrentUser
    {
        public UsuarioApi GetCurrentUser(ClaimsIdentity identity);
    }
}
