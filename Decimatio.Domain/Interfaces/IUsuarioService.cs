namespace Decimatio.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllUsers();
        Task<Usuario> GetById(int idUsuario);
    }
}
