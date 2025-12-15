namespace Gateway.Api.Dtos
{
    public class CreateCargaArchivoRequest
    {
        public IFormFile File { get; set; }
        public string Usuario { get; set; }
    }
}
