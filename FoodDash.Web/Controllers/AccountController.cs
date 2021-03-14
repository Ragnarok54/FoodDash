using FoodDash.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace FoodDash.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                // Check login details

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed");
                return View("~/Views/Account/Login.cshtml");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Register failed");
                return View("~/Views/Account/Register.cshtml");
            }
        }

        [HttpGet]
        public IActionResult Profile()
        {
            try
            {
                var model = new ProfileModel
                {
                    Email = "test@gmail.com",
                    FirstName = "Radu",
                    LastName = "Teodor",
                    Address = "Craiova, Str. Sperantei"
                };

                return View("~/Views/Account/Profile.cshtml", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "View profile failed");
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult Profile(ProfileModel model)
        {
            try
            {
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Edit profile failed");
                return RedirectToAction("Profile");
            }
        }
    }
}
