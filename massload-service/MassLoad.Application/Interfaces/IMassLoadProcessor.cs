using MassLoad.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassLoad.Application.Interfaces
{
    public interface IMassLoadProcessor
    {
        Task ProcessAsync(CargaMessage msg);
    }
}
