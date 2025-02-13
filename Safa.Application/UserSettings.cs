using Safa.Domain.Types;

namespace Safa.Application;

public record UserSettings(
    Guid UserId,
    SavingPreferences SavingPreferences);