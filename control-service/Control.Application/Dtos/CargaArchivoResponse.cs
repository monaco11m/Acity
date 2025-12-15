using Control.Domain.Enums;

namespace Control.Application.Dto
{
    public class CargaArchivoResponse
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public EstadoCarga Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

}
