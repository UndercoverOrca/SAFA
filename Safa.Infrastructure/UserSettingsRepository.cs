using Safa.Application;
using Safa.Domain.Extensions;
using Safa.Infrastructure.Factories;

namespace Safa.Infrastructure;

public class UserSettingsRepository : IUserSettingsRepository
{
    private readonly SafaDbContext context;

    public UserSettingsRepository(SafaDbContext context)
    {
        this.context = context;
    }

    public async Task<UserSettingsEditRequest> GetBy(Guid userId)
    {
        await using var dbContext = this.context;
        
        return await this.context.UserSettings
            .Where(x => x.UserEntityId == userId)
            .FirstOrNone()
            .MatchAsync(
                x => Task.FromResult(UserSettingsFactory.Convert(x)),
                () => Task.FromResult(new UserSettingsEditRequest(0m)));
    }
}