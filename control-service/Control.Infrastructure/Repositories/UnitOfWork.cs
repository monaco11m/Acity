using Control.Application.Interfaces;
using Control.Infrastructure.Persistence;

namespace Control.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ControlDbContext _context;

        public ICargaArchivoRepository CargaArchivos { get; }

        public UnitOfWork(
            ControlDbContext context,
            ICargaArchivoRepository cargaRepo)
        {
            _context = context;
            CargaArchivos = cargaRepo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
