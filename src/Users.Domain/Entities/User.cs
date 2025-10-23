using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Domain.Entities
{
    [Table("Usuario")]
    public sealed class User
    {
        public int IdUsuario { get; init; }
        public string Nombre { get; init; } = default!;
        public string Apellido { get; init; } = default!;
        public string Correo { get; init; } = default!;
        public DateTime FechaCreacion { get; init; }
        public bool Activo { get; init; }
    }
}
