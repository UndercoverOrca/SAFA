using LanguageExt.UnsafeValueAccess;
using Microsoft.AspNetCore.Mvc;
using Safa.Application;
using Safa.Domain;

namespace Safa.WebUi.Controllers.Cashbook;

[Route("cashbook")]
public class CashbookController : Controller
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public CashbookController(
        ITransactionRepository transactionRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        this.transactionRepository = transactionRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var userId = this.httpContextAccessor.HttpContext!.User.GetId();

        if (userId.IsNone)
        {
            return View();
        }

        var transactions = await this.transactionRepository.GetAll(userId.ValueUnsafe());
        return View(transactions);
    }
    
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Transaction transaction)
    {
        var userId = this.httpContextAccessor.HttpContext!.User.GetId();
        
        if (this.ModelState.IsValid || userId.IsNone)
        {
            await this.transactionRepository.Create(transaction, userId.ValueUnsafe());
            return RedirectToAction("Index");
        }
        
        return View(transaction);
    }
}