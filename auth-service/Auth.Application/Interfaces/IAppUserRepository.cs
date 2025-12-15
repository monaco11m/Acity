using Auth.Application.Dto;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetByUsernameAsync(string username);
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<AppUser> GetByIdAsync(string id);
    }
}
