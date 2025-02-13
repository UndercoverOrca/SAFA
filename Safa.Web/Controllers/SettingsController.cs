using LanguageExt.UnsafeValueAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safa.Application;
using Safa.WebUi;

namespace Safa.Web.Controllers;

[Authorize]
[Route("settings")]
public class SettingsController : Controller
{
    private readonly IUserSettingsRepository userSettingsRepository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public SettingsController(
        IUserSettingsRepository userSettingsRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        this.userSettingsRepository = userSettingsRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var userId = this.httpContextAccessor.HttpContext!.User.GetId();
        if (userId.IsNone)
        {
            return RedirectToAction("Index", "Home");
        }

        var userSettings = await userSettingsRepository
            .GetBy(userId.ValueUnsafe());
        
        return View(userSettings);
    }
}