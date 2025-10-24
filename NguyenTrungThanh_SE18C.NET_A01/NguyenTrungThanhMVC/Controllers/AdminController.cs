using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NguyenTrungThanhMVC.Models;
using Services.Interfaces;

namespace NguyenTrungThanhMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly INewsArticleService _newsArticleService; 

        public AdminController(IAccountService accountService, INewsArticleService newsArticleService)
        {
            _accountService = accountService;
            _newsArticleService = newsArticleService;
        }

        public IActionResult Index()
        {
            var accounts = _accountService.GetAccounts();
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateAccountPartial", new CreateAccountViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = new SystemAccount
                {
                    AccountName = model.AccountName,
                    AccountEmail = model.AccountEmail,
                    AccountPassword = model.AccountPassword,
                    AccountRole = model.AccountRole
                };
                _accountService.CreateAccount(account);
                return Json(new { success = true });
            }
            return PartialView("_CreateAccountPartial", model);
        }

        [HttpGet]
        public IActionResult Edit(short id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null) return NotFound();

            var model = new EditAccountViewModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = (short)account.AccountRole
            };
            return PartialView("_EditAccountPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, EditAccountViewModel model)
        {
            if (id != model.AccountId) return NotFound();

            if (ModelState.IsValid)
            {
                var account = _accountService.GetAccountById(id);
                if (account == null) return NotFound();

                account.AccountName = model.AccountName;
                account.AccountEmail = model.AccountEmail;
                account.AccountRole = model.AccountRole;

                _accountService.UpdateAccount(account);
                return Json(new { success = true });
            }
            return PartialView("_EditAccountPartial", model);
        }

        public IActionResult Details(short id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null) return NotFound();
            return View(account);
        }

        public IActionResult Delete(short id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null) return NotFound();
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            var account = _accountService.GetAccountById(id);
            if (account != null)
            {
                _accountService.DeleteAccount(account);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Report()
        {
            var model = new ReportViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Report(ReportViewModel model)
        {
            if (model.StartDate > model.EndDate)
            {
                ModelState.AddModelError("StartDate", "Start Date cannot be after End Date.");
            }

            if (ModelState.IsValid)
            {
                model.NewsArticles = _newsArticleService.GetNewsArticlesByPeriod(model.StartDate, model.EndDate);
            }

            return View(model);
        }
    }
}