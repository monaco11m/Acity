using Control.Application.Dtos;
using Control.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace Control.Application.Services
{
    public class SeaweedFsService : ISeaweedFsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _assignUrl;
        private readonly string _volumeUrl;

        public SeaweedFsService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _assignUrl = $"{configuration["SeaweedFS:MasterUrl"]}/dir/assign";
            _volumeUrl = configuration["SeaweedFS:VolumeUrl"];
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var assignJson = await _httpClient.GetStringAsync(_assignUrl);
            var assignResult = JsonSerializer.Deserialize<SeaweedUploadResponse>(assignJson);

            var uploadUrl = $"{_volumeUrl}/{assignResult.fid}";

            using var content = new MultipartFormDataContent();
            using var stream = file.OpenReadStream();

            content.Add(new StreamContent(stream), "file", file.FileName);

            var response = await _httpClient.PostAsync(uploadUrl, content);
            response.EnsureSuccessStatusCode();

            return assignResult.fid;
        }
    }
}
