

namespace Auth.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUsers { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        Task<int> SaveChangesAsync();
    }
}
