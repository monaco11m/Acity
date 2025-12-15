using Control.Domain.Entities;

public interface ICargaArchivoRepository
{
    Task AddAsync(CargaArchivo carga);
    Task<CargaArchivo?> GetByIdAsync(int id);
}
