namespace Safa.Application;

public interface IUserSettingsRepository
{
    Task<UserSettingsEditRequest> GetBy(Guid userId);
}