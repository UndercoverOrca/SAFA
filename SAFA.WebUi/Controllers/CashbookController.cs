using Microsoft.AspNetCore.Mvc;

namespace SAFA.WebUi;

public class CashbookController : Controller
{
    public IActionResult Index() =>
        View();
}