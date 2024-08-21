using Microsoft.AspNetCore.Mvc;
using Safa.Application;

namespace Safa.WebUi.Controllers.Cashbook;

[Route("cashbook")]
public class CashbookController : Controller
{
    private readonly ITransactionRepository transactionRepository;

    public CashbookController(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var transactions = await this.transactionRepository.GetAll();
        
        return View(transactions);
    }
}