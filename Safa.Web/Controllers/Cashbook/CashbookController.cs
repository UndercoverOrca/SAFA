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
        // var userId = this.httpContextAccessor.HttpContext!.User.GetId();
        var userId = new Guid("B26AEE2D-086D-4D5E-9B11-3636922B7966");
        
        // if (userId.IsNone)
        // {
        //     return View(new List<Transaction>());
        // }

        var transactions = await this.transactionRepository.GetAll(userId);
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
        // var userId = this.httpContextAccessor.HttpContext!.User.GetId();
        var userId = new Guid("B26AEE2D-086D-4D5E-9B11-3636922B7966");
        
        if (this.ModelState.IsValid)
        {
            await this.transactionRepository.Create(transaction, userId);
            return RedirectToAction("Index");
        }
        
        return View(transaction);
    }
}