using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly AuthDbContext _context;

        public AppUserRepository(UserManager<AppUser> userManager, AuthDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}
