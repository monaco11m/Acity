using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RefreshToken> GenerateAsync(AppUser user)
        {
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var refreshToken = new RefreshToken
            {
                Token = token,
                UserId = user.Id,
                ExpireAt = DateTime.UtcNow.AddDays(7)
            };

            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return refreshToken;
        }
    }
}
