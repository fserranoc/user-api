using Users.Domain.Entities;
namespace Users.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Crea un usuario en la bd
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> CreateAsync(string nombre, string apellido, string correo, CancellationToken ct);

        /// <summary>
        /// Lista los usuarios de la bd
        /// </summary>
        /// <param name="incluirInactivos"></param>
        /// <param name="buscarTexto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IReadOnlyList<User>> ListAsync(bool incluirInactivos, string? buscarTexto, CancellationToken ct);

        /// <summary>
        /// Obtiene un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<User?> GetAsync(int idUsuario, CancellationToken ct);

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="activo"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(int idUsuario, string nombre, string apellido, string correo, bool activo, CancellationToken ct);
        
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(int idUsuario, CancellationToken ct);
    }
}
