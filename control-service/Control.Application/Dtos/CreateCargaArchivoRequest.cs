using Microsoft.AspNetCore.Http;

namespace Control.Application.Dto
{
    public class CreateCargaArchivoRequest
    {
        public IFormFile File { get; set; }
        public string Usuario { get; set; } = string.Empty;
    }

}
