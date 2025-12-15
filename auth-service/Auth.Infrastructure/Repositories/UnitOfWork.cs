using Auth.Application.Interfaces;
using Auth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthDbContext _context;

        public IAppUserRepository AppUsers { get; }
        public IRefreshTokenRepository RefreshTokens { get; }


        public UnitOfWork
        (
            AuthDbContext context,

            IAppUserRepository appUserRepository,
             IRefreshTokenRepository refreshTokenRepository
        )
        {
            _context = context;
            AppUsers = appUserRepository;
            RefreshTokens = refreshTokenRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
