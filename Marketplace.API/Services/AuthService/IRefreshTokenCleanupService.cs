namespace Marketplace.Services.AuthService;

public interface IRefreshTokenCleanupService
{
    public Task Execute();
}