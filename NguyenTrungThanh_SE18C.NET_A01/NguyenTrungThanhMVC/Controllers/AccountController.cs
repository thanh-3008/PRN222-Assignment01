using BusinessObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NguyenTrungThanhMVC.Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NguyenTrungThanhMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SystemAccount? account = null;
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];

            if (model.Email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) && model.Password.Equals(adminPassword))
            {
                account = new SystemAccount { AccountId = 0, AccountEmail = adminEmail, AccountRole = 0 }; // 0: Admin
            }
            else
            {
                account = _accountService.GetAccountByEmail(model.Email);
                if (account == null || account.AccountPassword != model.Password)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(model);
                }
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.AccountEmail),
                new Claim(ClaimTypes.Email, account.AccountEmail),
                new Claim("AccountId", account.AccountId.ToString()),
                new Claim(ClaimTypes.Role, account.AccountRole == 0 ? "Admin" : (account.AccountRole == 1 ? "Staff" : "Lecturer"))
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            if (account.AccountRole == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Profile(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var accountIdClaim = User.FindFirst("AccountId");
            if (accountIdClaim == null || !short.TryParse(accountIdClaim.Value, out short accountId))
            {
                return Forbid();
            }

            var account = _accountService.GetAccountById(accountId);
            if (account == null)
            {
                return NotFound();
            }

            if (account.AccountPassword != model.OldPassword)
            {
                ModelState.AddModelError("OldPassword", "Current password is not correct.");
                return View(model);
            }

            account.AccountPassword = model.NewPassword;
            _accountService.UpdateAccount(account);

            ViewBag.SuccessMessage = "Your password has been changed successfully.";
            return View();
        }
    }
}