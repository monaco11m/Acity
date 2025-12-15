using Control.Application.Dto;
using Control.Application.Dtos;
using Control.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Control.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargaArchivoController : ControllerBase
    {
        private readonly ICargaArchivoService _service;

        public CargaArchivoController(ICargaArchivoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromForm] CreateCargaArchivoRequest request)
        {
            var result = await _service.CrearCargaAsync(request);
            return Ok(result);
        }

        [AllowAnonymous] 
        [HttpPost("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarCargaEstadoRequest req)
        {
            bool ok = await _service.ActualizarEstadoAsync(req);

            if (!ok) return NotFound();

            return Ok();
        }

    }
}
