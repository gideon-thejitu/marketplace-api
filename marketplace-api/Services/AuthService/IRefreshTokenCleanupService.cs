namespace marketplace_api.Services.Auth;

public interface IRefreshTokenCleanupService
{
    public Task Execute();
}