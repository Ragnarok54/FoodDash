using FoodDash.Web.Common.Enums;
using FoodDash.Web.DataAccess;
using FoodDash.Web.DataAccess.Entities;
using FoodDash.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FoodDash.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly Context _context;

        public AccountController(ILogger<AccountController> logger, Context context)
        {
            _logger = logger;
            _context = context;
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
                //_context.Users.Find

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
                var newUser = new User
                {
                    Address = model.Address,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    UserRoleId = (int)UserRoleType.Customer
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

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
                var user = _context.Users.FirstOrDefault();

                var model = new ProfileModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address
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
                //var user = _context.Users.Find(model.userId)
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
