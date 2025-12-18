

using Auth.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Auth.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext<AppUser, Role, string>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {

        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
