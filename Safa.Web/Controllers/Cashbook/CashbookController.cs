using LanguageExt.UnsafeValueAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safa.Application;
using Safa.Domain;
using Safa.Web.Models;

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
        if (userId.IsNone)
        {
            return View(new CashbookViewModel(new List<Transaction>(), 0, 0, 0));
        }

        var transactions = await transactionRepository.GetAll(userId);
        
        var totalIncome = transactions
            .Where(x => x.Type == TypeOfTransaction.Income)
            .Sum(x => x.Amount);
        
        var totalExpenses = transactions
            .Where(x => x.Type == TypeOfTransaction.Expense)
            .Sum(x => x.Amount);
        
        var cashbookViewModel = new CashbookViewModel(
            transactions,
            totalIncome,
            totalExpenses,
            totalIncome - totalExpenses);
        
        return View(cashbookViewModel);
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
    
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var userId = this.httpContextAccessor.HttpContext!.User.GetId();
        if (userId.IsNone)
        {
            return RedirectToAction("Index");
        }

        var transaction = await this.transactionRepository.GetBy(id, userId.ValueUnsafe());
        return transaction.Match<IActionResult>(
            View,
            () => RedirectToAction("Index"));
    }
    
    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Transaction transaction)
    {
        var userId = this.httpContextAccessor.HttpContext!.User.GetId();
        
        if (this.ModelState.IsValid && userId.IsSome)
        {
            await this.transactionRepository.Update(transaction, userId.ValueUnsafe());
            return RedirectToAction("Index");
        }
        
        return View(transaction);
    }
}