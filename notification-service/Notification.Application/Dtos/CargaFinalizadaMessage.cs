using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Dtos
{
    public class CargaFinalizadaMessage
    {
        public int CargaId { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string FileId { get; set; } = string.Empty;
    }
}
