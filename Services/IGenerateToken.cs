using APITest_01.Models.DB;

namespace APITest_01.Services
{
    public interface IGenerateToken
    {
        public string GetToken(UsuarioApi user);

    }
}
