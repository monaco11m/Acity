using Gateway.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AuthProxyController(IHttpClientFactory factory, IConfiguration config)
        {
            _httpClient = factory.CreateClient();
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var baseUrl = _config["Services:AuthService:BaseUrl"];
            var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/auth/login", request);

            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAppUserRequest request)
        {
            var baseUrl = _config["Services:AuthService:BaseUrl"];
            var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/auth/create", request);

            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }
    }
}
