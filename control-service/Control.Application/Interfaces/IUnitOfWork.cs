using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICargaArchivoRepository CargaArchivos { get; }
        Task<int> SaveChangesAsync();
    }
}
