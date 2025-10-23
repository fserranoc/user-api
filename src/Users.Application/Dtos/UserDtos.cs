namespace Users.Application.Dtos
{
    public sealed class UserDtos
    {
        public sealed record CreateUserRequest(string Nombre, string Apellido, string Correo);
        public sealed record CreateUserResponse(int IdUsuario);


        public sealed record UserListRequest(bool IncluirInactivos = false, string? BuscarTexto = null);
        public sealed record UserItem(int IdUsuario, string Nombre, string Apellido, string Correo, DateTime FechaCreacion, bool Activo);


        public sealed record GetUserRequest(int IdUsuario);


        public sealed record UpdateUserRequest(int IdUsuario, string Nombre, string Apellido, string Correo, bool Activo);


        public sealed record DeleteUserRequest(int IdUsuario);
    }
}
