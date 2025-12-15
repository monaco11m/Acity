using AutoMapper;
using Control.Application.Dto;
using Control.Application.Dtos;
using Control.Application.Interfaces;
using Control.Domain.Entities;
using Control.Domain.Enums;

namespace Control.Application.Services
{
    public class CargaArchivoService : ICargaArchivoService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISeaweedFsService _seaweed;
        private readonly IRabbitPublisher _publisher;


        public CargaArchivoService
        (
            ISeaweedFsService seaweed,
            IRabbitPublisher publisher,
            IUnitOfWork unit
        )
        {
            _seaweed = seaweed;
            _publisher = publisher;
            _uow = unit;
        }

        public async Task<CargaArchivoResponse> CrearCargaAsync(CreateCargaArchivoRequest request)
        {
            string fileId = await _seaweed.UploadFileAsync(request.File);

            var carga = new CargaArchivo
            {
                NombreArchivo = request.File.FileName,
                Usuario = request.Usuario,
                Estado = EstadoCarga.Pendiente,
                FileId = fileId
            };

            await _uow.CargaArchivos.AddAsync(carga);
            await _uow.SaveChangesAsync();


            await _publisher.PublishAsync(new
            {
                CargaId = carga.Id,
                FileId = fileId,
                Usuario = carga.Usuario
            });

            return new CargaArchivoResponse
            {
                Id = carga.Id,
                NombreArchivo = carga.NombreArchivo,
                Estado = carga.Estado,
                FechaRegistro = carga.FechaRegistro
            };
        }

        public async Task<bool> ActualizarEstadoAsync(ActualizarCargaEstadoRequest req)
        {
            var carga = await _uow.CargaArchivos.GetByIdAsync(req.Id);

            if (carga == null)
                return false;

            carga.Estado = req.Estado;
            carga.FechaFin = DateTime.UtcNow;

            await _uow.SaveChangesAsync();
            return true;
        }



    }
}
