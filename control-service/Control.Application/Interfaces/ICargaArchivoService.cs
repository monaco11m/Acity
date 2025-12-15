using Control.Application.Dto;
using Control.Application.Dtos;

namespace Control.Application.Interfaces
{
    public interface ICargaArchivoService
    {
        Task<CargaArchivoResponse> CrearCargaAsync(CreateCargaArchivoRequest request);

        Task<bool>ActualizarEstadoAsync(ActualizarCargaEstadoRequest req);
    }
}
