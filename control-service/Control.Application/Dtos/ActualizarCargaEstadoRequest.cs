using Control.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Application.Dtos
{
    public class ActualizarCargaEstadoRequest
    {
        public int Id { get; set; }
        public EstadoCarga Estado { get; set; }
    }
}
