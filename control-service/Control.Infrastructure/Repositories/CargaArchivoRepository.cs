using Control.Domain.Entities;
using Control.Application.Interfaces;
using Control.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Control.Infrastructure.Repositories
{
    public class CargaArchivoRepository : ICargaArchivoRepository
    {
        private readonly ControlDbContext _context;

        public CargaArchivoRepository(ControlDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CargaArchivo carga)
        {
            await _context.CargaArchivos.AddAsync(carga);
        }

        public async Task<CargaArchivo?> GetByIdAsync(int id)
        {
            return await _context.CargaArchivos.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
