using LanguageExt.UnsafeValueAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safa.Application;
using Safa.Domain;

namespace Safa.WebUi.Controllers.Cashbook;

[Authorize]
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

        return userId.IsSome
            ? View(await transactionRepository.GetAll(userId))
            : View(new List<Transaction>());
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
        
        if (this.ModelState.IsValid && userId.IsSome)
        {
            await this.transactionRepository.Create(transaction, userId.ValueUnsafe());
            return RedirectToAction("Index");
        }
        
        return View(transaction);
    }
}