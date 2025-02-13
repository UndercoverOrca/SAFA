using Safa.Application;
using Safa.Domain.Types;
using Safa.Infrastructure.Entities;

namespace Safa.Infrastructure.Factories;

public static class UserSettingsFactory
{
    public static UserSettingsEditRequest Convert(UserSettingsEntity entity)
    {
        var userSettings = new UserSettings( 
            entity.UserEntityId,
            new SavingPreferences(Fraction.TryCreate(entity.SavingFraction)
                .Match(
                    x => x,
                    Fraction.Zero)));

        return Convert(userSettings);
    }

    private static UserSettingsEditRequest Convert(UserSettings entity) =>
        new(entity.SavingPreferences.SavingFraction.Value);

}