
using Dapper;
using System.Data;
using Users.Domain.Entities;
using Users.Domain.Interfaces.Repository;
using Users.Infrastructure.Data;

namespace Users.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ISqlConnectionFactory _factory;

        public UserRepository(ISqlConnectionFactory factory) => _factory = factory;

        public async Task<int> CreateAsync(string nombre, string apellido, string correo, CancellationToken ct)
        {
            using var conn = _factory.CreateConnection();         
            var id = await conn.ExecuteScalarAsync<int>(
            sql: Constants.SP_CREATE,
            param: new { Nombre = nombre, Apellido = apellido, Correo = correo },
            commandType: CommandType.StoredProcedure);
            return id;
        }

        public async Task<IReadOnlyList<User>> ListAsync(bool incluirInactivos, string? buscarTexto, CancellationToken ct)
        {
            using var conn = _factory.CreateConnection();
            var rows = await conn.QueryAsync<User>(
            sql: Constants.SP_LIST,
            param: new { IncluirInactivos = incluirInactivos, BuscarTexto = buscarTexto },
            commandType: CommandType.StoredProcedure);
            return rows.ToList();
        }

        public async Task<User?> GetAsync(int idUsuario, CancellationToken ct)
        {
            using var conn = _factory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(
            sql: Constants.SP_GET,
            param: new { IdUsuario = idUsuario },
            commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(int idUsuario, string nombre, string apellido, string correo, bool activo, CancellationToken ct)
        {
            using var conn = _factory.CreateConnection();
            var affected = await conn.ExecuteScalarAsync<int>(
            sql: Constants.SP_UPDATE,
            param: new { IdUsuario = idUsuario, Nombre = nombre, Apellido = apellido, Correo = correo, Activo = activo },
            commandType: CommandType.StoredProcedure);
            return affected;
        }

        public async Task<int> DeleteAsync(int idUsuario, CancellationToken ct)
        {
            using var conn = _factory.CreateConnection();
            var affected = await conn.ExecuteScalarAsync<int>(
            sql: Constants.SP_DELETE,
            param: new { IdUsuario = idUsuario },
            commandType: CommandType.StoredProcedure);
            return affected;
        }
    }
}
