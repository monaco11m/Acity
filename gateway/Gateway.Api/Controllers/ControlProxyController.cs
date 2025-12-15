using Gateway.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("control")]
    [Authorize]
    public class ControlProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ControlProxyController(IHttpClientFactory factory, IConfiguration config)
        {
            _httpClient = factory.CreateClient();
            _config = config;
        }

        [Authorize]
        [HttpPost("control/carga/crear")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CrearCarga([FromForm] CreateCargaArchivoRequest request)
        {
            var baseUrl = _config["Services:ControlService:BaseUrl"];

            using var form = new MultipartFormDataContent();

            var streamContent = new StreamContent(request.File.OpenReadStream());
            streamContent.Headers.ContentType =
                new MediaTypeHeaderValue(request.File.ContentType);

            form.Add(streamContent, "File", request.File.FileName);
            form.Add(new StringContent(request.Usuario), "Usuario");

            var httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                $"{baseUrl}/api/CargaArchivo/crear"
            );

            httpRequest.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer",
                    Request.Headers.Authorization.ToString().Replace("Bearer ", ""));

            httpRequest.Content = form;

            var response = await _httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, content);
        }
    }
}
