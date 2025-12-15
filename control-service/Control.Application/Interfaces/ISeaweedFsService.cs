using Microsoft.AspNetCore.Http;

namespace Control.Application.Interfaces
{
    public interface ISeaweedFsService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }

}
