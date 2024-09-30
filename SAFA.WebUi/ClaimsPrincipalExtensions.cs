using System.Security.Claims;
using LanguageExt;

namespace Safa.WebUi;

public static class ClaimsPrincipalExtensions
{
    public static Option<Guid> GetId(this ClaimsPrincipal principal) =>
        principal
            .FindFirst(ClaimTypes.NameIdentifier)
            .IsNull()
            ? Prelude.None
            : Prelude.Some(Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)));
}