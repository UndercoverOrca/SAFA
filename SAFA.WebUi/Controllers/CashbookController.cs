using Microsoft.AspNetCore.Mvc;

namespace SAFA.WebUi.Controllers;

public class CashbookController : Controller
{
    public IActionResult Index() =>
        View();
}