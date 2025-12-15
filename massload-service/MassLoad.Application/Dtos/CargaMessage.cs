using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassLoad.Application.Dtos
{
    public class CargaMessage
    {
        public int CargaId { get; set; }
        public string FileId { get; set; }
        public string Usuario { get; set; } = string.Empty;
    }
}
