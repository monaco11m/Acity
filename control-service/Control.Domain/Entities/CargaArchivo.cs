using Control.Domain.Enums;

namespace Control.Domain.Entities
{
    public class CargaArchivo
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public EstadoCarga Estado { get; set; } = EstadoCarga.Pendiente;
        public DateTime? FechaFin { get; set; }

        public string FileId { get; set; } = string.Empty;
    }

}
