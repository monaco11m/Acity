using Auth.Application.Dto;
using Auth.Application.Exceptions;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AppUserService(IUnitOfWork repository, IMapper mapper, IJwtService jwtService, IRefreshTokenService refreshTokenService  )
        {
            _repository = repository;
            _mapper = mapper;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _repository.AppUsers.GetByUsernameAsync(request.UserName);

            if (user == null || !await _repository.AppUsers.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedException("Invalid username or password.");
            }

            var userDto = await MapUserRole(user);

            string token = _jwtService.GenerateJwtToken(user);
            var refresh = await _refreshTokenService.GenerateAsync(user);

            LoginResponse response = new LoginResponse
            {
                Token = token,
                User = userDto,
                RefreshToken = refresh.Token,

            };

            return response;
        }

        public async Task<AppUserDto> CreateAsync(CreateAppUserRequest request)
        {
            var user = _mapper.Map<AppUser>(request.User);
            var result = await _repository.AppUsers.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                ThrowException(result);
            }
            

            return await MapUserRole(user);
        }

        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
        {
            var stored = await _repository.RefreshTokens.GetByTokenAsync(refreshToken);

            if (stored == null || stored.ExpireAt < DateTime.UtcNow)
                throw new UnauthorizedException("Invalid refresh token");

            var user = await _repository.AppUsers.GetByIdAsync(stored.UserId);

            string newJwt = _jwtService.GenerateJwtToken(user);
            var newRefresh = await _refreshTokenService.GenerateAsync(user);

            await _repository.RefreshTokens.InvalidateAsync(stored);
            await _repository.SaveChangesAsync();

            return new LoginResponse
            {
                Token = newJwt,
                RefreshToken = newRefresh.Token,
                User = _mapper.Map<AppUserDto>(user)
            };
        }

        private async Task<AppUserDto> MapUserRole(AppUser user)
        {
            var userDto = _mapper.Map<AppUserDto>(user);
            return userDto;
        }

        private void ThrowException(IdentityResult result)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new DomainValidationException(errors);
        }

        public async Task<AppUserDto> GetByIdAsync(string id)
        {
            var user = await _repository.AppUsers.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException($"User with id {id} not found.");

            var dto = _mapper.Map<AppUserDto>(user);
            return dto;
        }
    }
}
