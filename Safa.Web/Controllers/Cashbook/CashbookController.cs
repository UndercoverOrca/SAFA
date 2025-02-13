using LanguageExt.UnsafeValueAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safa.Application;
using Safa.Domain.Types;
using Safa.Web.Models;
using Safa.WebUi;

namespace Safa.Web.Controllers.Cashbook;

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
            return View(new CashbookViewModel(
                new List<Transaction>(),
                new AccountSummary(
                    Amount.Zero, 
                    Amount.Zero, 
                    Amount.Zero)));
        }

        var transactions = await transactionRepository
            .GetAll(userId)
            .Match(
                x => x,
                () => new List<Transaction>());
        
        //TODO: DELETE AFTER IMPLEMENTING ACCOUNT SUMMARY
        var savingFraction = Fraction.TryCreate(0.75m)
            .Match(
                fraction => fraction,
                Fraction.Zero);
        var savingPreferences = new SavingPreferences(savingFraction);

        var accountSummary = AccountSummary.CreateFromTransactions(transactions, savingPreferences);
        
        var cashbookViewModel = new CashbookViewModel(
            transactions,
            accountSummary);
        
        return View(cashbookViewModel);
    }
    
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TransactionRequest transaction)
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
    
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TransactionRequest transaction)
    {
        var userId = this.httpContextAccessor.HttpContext!.User.GetId();
        
        if (this.ModelState.IsValid && userId.IsSome)
        {
            await this.transactionRepository.Update(transaction, userId.ValueUnsafe());
            return RedirectToAction("Index");
        }
        
        return View(transaction);
    }
    
    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
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
    
    // Change to Transaction instead of TransactionRequest? Might go hand-in-hand with using a service
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(TransactionRequest transaction)
    {
        var userId = this.httpContextAccessor.HttpContext!.User.GetId();
        
        if (this.ModelState.IsValid && userId.IsSome)
        {
            await this.transactionRepository.Delete(transaction, userId.ValueUnsafe());
            return RedirectToAction("Index");
        }
        
        return View(transaction);
    }
}