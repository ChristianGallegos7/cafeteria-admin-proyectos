namespace Cafeteria.Backend.Token
{
    public interface IGenerateToken
    {
        Task<string> GenerarToken(int idUsuario, string correo, int idRol, string nombreRol);
    }
}
