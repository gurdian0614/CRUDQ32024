
using SQLite;

namespace CRUDQ32024.Models
{
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nombre { get; set; }

        [NotNull]
        public string Email { get; set; }

        public string Direccion { get; set; }
    }
}
