using Users.Application.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Interfaces.Repository;
using static Users.Application.Dtos.UserDtos;

namespace Users.Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public Task<int> CreateAsync(CreateUserRequest request, CancellationToken ct)
        => _repo.CreateAsync(request.Nombre, request.Apellido, request.Correo, ct);

        public async Task<IReadOnlyList<UserItem>> ListAsync(UserListRequest request, CancellationToken ct)
        {
            var list = await _repo.ListAsync(request.IncluirInactivos, request.BuscarTexto, ct);
            return list.Select(Map).ToList();
        }

        public async Task<UserItem?> GetAsync(GetUserRequest request, CancellationToken ct)
        {
            var user = await _repo.GetAsync(request.IdUsuario, ct);
            return user is null ? null : Map(user);
        }

        public async Task<bool> UpdateAsync(UpdateUserRequest request, CancellationToken ct)
        => (await _repo.UpdateAsync(request.IdUsuario, request.Nombre, request.Apellido, request.Correo, request.Activo, ct)) > 0;


        public async Task<bool> DeleteAsync(DeleteUserRequest request, CancellationToken ct)
        => (await _repo.DeleteAsync(request.IdUsuario, ct)) > 0;

        private static UserItem Map(User u) => new(u.IdUsuario, u.Nombre, u.Apellido, u.Correo, u.FechaCreacion, u.Activo);
    }
}
