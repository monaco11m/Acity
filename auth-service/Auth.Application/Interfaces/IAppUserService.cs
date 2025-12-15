using Auth.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Interfaces
{
    public interface IAppUserService
    {
        Task<AppUserDto> CreateAsync(CreateAppUserRequest user);

        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task<AppUserDto> GetByIdAsync(string id);

        Task<LoginResponse> RefreshTokenAsync(string refreshToken);


    }
}
