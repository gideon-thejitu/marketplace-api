using Marketplace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.AuthService;

public class RefreshTokenCleanupService : IRefreshTokenCleanupService
{
    private readonly DataContext _dataContext;
    private readonly ILogger<RefreshTokenCleanupService> _logger;

    public RefreshTokenCleanupService(DataContext dataContext, ILogger<RefreshTokenCleanupService> logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

    public async Task Execute()
    {
        var refreshTokens = _dataContext.RefreshTokens.Where(refreshToken =>
            refreshToken.Expiry <= DateTime.Now && refreshToken.DeletedAt == null);
        var count = await refreshTokens.CountAsync();
        _logger.LogInformation($"Found {count} refresh tokens!");

        await refreshTokens.ExecuteUpdateAsync(refreshToken =>
            refreshToken.SetProperty(property => property.DeletedAt, DateTime.Now));
        
        _logger.LogInformation($"Refresh Tokens cleared!");
    }
}